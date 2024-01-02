using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using project1_backend.Models;
using project1_backend.Models.Custom;
using project1_backend.Service;
using Microsoft.AspNetCore.SignalR;
namespace project1_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonhangsController : ControllerBase
    {
        private readonly ProjectBongDaContext _context;

        public DonhangsController(ProjectBongDaContext context)
        {
            _context = context;
        }

        [HttpGet("ThanhToanQr")]
        public async Task<IActionResult> ThanhToanQR(String phone,int id, string time)
        {
            if (_context.Donhangs == null)
            {
                return NotFound();
            }
            var donhang= await _context.Donhangs.FirstOrDefaultAsync(c=>c.Orderid==id);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Phonenumber == phone);
            if (!DateTime.TryParse(time, out DateTime qrTime))
            {
                return BadRequest("Invalid time format");
            }

            TimeSpan timeDifference = DateTime.Now - qrTime;

            if (timeDifference.Duration() > TimeSpan.FromMinutes(1))
            {
                return Ok(new { message = "Mã QR đã hết hạn!" });
            }

            if (donhang != null)
            {
               // var field=await _context.Sanbongs.FirstOrDefaultAsync(s=>s.Fieldid==don)
                donhang.Status = "Đặt thành công, đã thanh toán";
                await _context.SaveChangesAsync();
                var thongbao = new Thongbao();
                thongbao.Orderid = id;
                thongbao.Message = user.Name + " vừa thanh toán tiền sân" ;
                thongbao.Time = DateTime.Now;
                thongbao.Type = "1";

                if (_context.Thongbaos != null)
                {
                    _context.Thongbaos.Add(thongbao);

                    await _context.SaveChangesAsync();

                }
            }
            return Ok(new { message = "Bạn đã thanh toán thành công", success = true });
        }
        [HttpGet("KiemtraThanhtoan")]
        public async Task<IActionResult> KiemTraThanhToan(int id)
        {
            if (_context.Donhangs == null)
            {
                return NotFound();
            }
            var donhang = await _context.Donhangs.FirstOrDefaultAsync(c => c.Orderid == id);
           
            if (donhang != null && donhang.Status!=null)
            {
                if(donhang.Status.Contains("đã thanh toán"))
                {
                    return Ok(new { message = "Bạn đã thanh toán thành công", success = true });

                }
                // var field=await _context.Sanbongs.FirstOrDefaultAsync(s=>s.Fieldid==don)
            }
            return Ok(new { message = "Bạn chưa thanh toán thành công", success = false });
        }
        // GET: api/Donhangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetDonhangs(String by,String time)
        {
          if (_context.Donhangs == null)
          {
              return NotFound();
          }
          var donhang_by=new List<Donhang>();
          var donhang_time=new List<Donhang>();
          if(by.ToUpper() == "TẤT CẢ") {
                donhang_by =await _context.Donhangs.ToListAsync();
                donhang_by.RemoveAll(dh =>
                {
                    var dhang = _context.SamphamDonhangs.Any(d => d.Orderid == dh.Orderid);
                    return !dhang;
                });
            }
            else
            {
                donhang_by=await _context.Donhangs.Where(d=>d.Status.ToUpper()==by.ToUpper()).ToListAsync();
                donhang_by.RemoveAll(dh =>
                {
                    var dhang = _context.SamphamDonhangs.Any(d => d.Orderid == dh.Orderid);
                    return !dhang;
                });
            }
          if (time.ToUpper() == "1 TUẦN")
            {
                DateTime twoWeekAgo = DateTime.Now.AddDays(-7);
                donhang_time =await _context.Donhangs.Where(d=> d.Time >= twoWeekAgo && d.Time <= DateTime.Now).ToListAsync();
            }
          else if(time.ToUpper() =="15 NGÀY")
            {
                DateTime oneWeekAgo = DateTime.Now.AddDays(-15);
                donhang_time = await _context.Donhangs.Where(d => d.Time >= oneWeekAgo && d.Time <= DateTime.Now).ToListAsync();

            }
            else if (time.ToUpper() == "1 THÁNG")
            {
                DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);

                donhang_time = await _context.Donhangs
                    .Where(d => d.Time >= oneMonthAgo && d.Time <= DateTime.Now)
                    .ToListAsync();
            }
            else { 
                donhang_time= await _context.Donhangs.ToListAsync();
              }
          
            var intesection= donhang_by.Intersect(donhang_time).OrderByDescending(d=>d.Orderid).ToList();
           
            var result = intesection.Select(order =>
            {
                var spdh = _context.SamphamDonhangs.Where(sp => sp.Orderid == order.Orderid).ToList();
                var listsp = spdh.Select(s =>
                {
                    var p = _context.Products.FirstOrDefault(p => p.Productid == s.Productid);
                   
                    return new { ProductName = p.Productname, Quantity = s.Quantity,Cost=p.Price,Img=p.Linkimg };
                }).ToList();
                var u = _context.Users.FirstOrDefault(u => u.Phonenumber == order.Phonenumber);
                // Tạo một đối tượng mới bằng cách giữ lại tất cả thuộc tính của đối tượng 'inte'
                var inteWithListSP = new
                {
                    order,
                    listsp,
                    user=u
                };

                return inteWithListSP;
            }).ToList();
            return result;
        }

        // GET: api/Donhangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetDonhang(int id)
        {
          if (_context.Donhangs == null)
          {
              return NotFound();
          }
            var donhang = await _context.Donhangs.FindAsync(id);
            var sanpham= await _context.SamphamDonhangs.Where(s=>s.Orderid==id).ToListAsync();
            var donhangg=new { donhang, sanpham };
            if (donhang == null)
            {
                return NotFound();
            }

            return donhangg;
        }
        [HttpGet("allOPofUser")]
        public async Task<ActionResult<object>> GetAllDonhang(string id)
        {
            if (_context.Donhangs == null)
            {
                return NotFound();
            }
            
            var user=await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var donhangs= await _context.Donhangs.Where(d=>d.Phonenumber==id).ToListAsync();
            var list=new List<object>();
            foreach(var spdh in donhangs)
            {
                var sanpham = await _context.SamphamDonhangs.Where(s => s.Orderid == spdh.Orderid ).ToListAsync();
                var chitiet= new List<object>();
                foreach (var sp in sanpham)
                {
                    var product = await _context.Products.FindAsync(sp.Productid);
                    if (product != null)
                    {
                        chitiet.Add(product);
                    }
                }
                
                var donhangWithSanpham = new { Donhang = spdh, Sanpham = sanpham,Chitiet=chitiet };
                if (sanpham.Count > 0)
                {
                    list.Add(donhangWithSanpham);
                }
            }
            if (user == null)
            {
                return NotFound();
            }

            return list.OrderByDescending(item=> { dynamic obj = item; return obj.Donhang.Orderid; }).ToList() ;
        }

        [HttpGet("allOFofUser")]
        public async Task<ActionResult<object>> GetAllDonhangSB(string id)
        {
            if (_context.Donhangs == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var donhangs = await _context.Donhangs.Where(d => d.Phonenumber == id).ToListAsync();
            var list = new List<object>();
            foreach (var spdh in donhangs)
            {
                var sanpham = await _context.SamphamDonhangs.Where(s => s.Orderid == spdh.Orderid).ToListAsync();
                var sanbongdonhang = await _context.SanbongDonhangs.FirstOrDefaultAsync(sdh => sdh.Orderid == spdh.Orderid);
                var sanbong =new Sanbong();
                if(sanbongdonhang != null) {
                     sanbong = await _context.Sanbongs.FirstOrDefaultAsync(s => s.Fieldid == sanbongdonhang.Fieldid);
                }
                await Console.Out.WriteLineAsync(sanbongdonhang?.Fieldid);
                var donhang = new { Donhang = spdh,sanbongdh=sanbongdonhang,sanbong=sanbong};
                if (sanpham.Count == 0)
                {
                    list.Add(donhang);
                }
            }
            if (user == null)
            {
                return NotFound();
            }

            return list.OrderByDescending(item => { dynamic obj = item; return obj.Donhang.Orderid; }).ToList();
        }
        // PUT: api/Donhangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonhang(int id, Donhang donhang)
        {
            if (id != donhang.Orderid)
            {
                return BadRequest();
            }

            _context.Entry(donhang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonhangExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateStatus(int orderid, string Status)
        {
            if (_context.Donhangs == null)
            {
                return NotFound();
            }
            var dh = await _context.Donhangs.FindAsync(orderid);
            if (dh != null)
            {
                dh.Status = Status;
            }
            try
            {
               await  _context.SaveChangesAsync();
            }catch (DbUpdateConcurrencyException) { }
            return NoContent();
        }
        //Post: api/Donhangs/Huydon
        [HttpPost("HuyDon")]
        public async Task<IActionResult> HuyDonHang(int orderid)
        {
            if (_context.Donhangs == null)
            {
                return NotFound();

            }
            try
            {
                var donhang = await _context.Donhangs.FindAsync(orderid);
                if (donhang != null)
                {
                    donhang.Status = "Đã hủy";
                }
                var sanphamdonhang= _context.SamphamDonhangs.Where(s=>s.Orderid == orderid).ToList();
                if(sanphamdonhang.Count > 0)
                {
                    foreach (var item in sanphamdonhang)
                    {
                        var productKH = _context.Khohangs.FirstOrDefault(i=>i.Productid==item.Productid);
                        if (productKH != null)
                        {
                            productKH.Quantity += item.Quantity;
                        }
                    }
                }
                await _context.SaveChangesAsync();
                var thongbao = new Thongbao();
                var user = await _context.Users.FindAsync(donhang.Phonenumber);
                thongbao.Orderid = orderid;
                thongbao.Message = user.Name+ " vừa hủy đơn hàng " + orderid;
                thongbao.Time = DateTime.Now;
                thongbao.Type = "2";
                if (_context.Thongbaos != null)
                {
                    _context.Thongbaos.Add(thongbao);

                    await _context.SaveChangesAsync();

                }
                    return StatusCode(201, new
                {

                    Success = true,
                    Message = "Bạn đã hủy thành công đơn hàng!"
                });
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            
            return NoContent();
        }
        // POST: api/Donhangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("OrderProducts")]
        public async Task<ActionResult<Donhang>> PostDonhangProduct(List<InfoProduct> list,string phone,int ship)
        {
          if (_context.Donhangs == null)
          {
              return Problem("Entity set 'ProjectBongDaContext.Donhangs'  is null.");
          }
            var user = await _context.Users.FindAsync(phone);
            if (user == null)
            {
                return Ok(new { message="Đặt hàng thất bại!",success = false });
            }
            foreach (var product in list)
            {
                var khohang= await _context.Khohangs.FindAsync(product.ProductId);
                if (khohang?.Quantity < product.Quantity)
                {
                    return Ok(new {message="Sản phẩm "+product.ProductName+" chỉ còn "+khohang.Quantity+" sản phẩm trong kho hàng",success=false});
                }
            }
            var donhang=new Donhang();
            donhang.Orderid = 0;
            donhang.Phonenumber=user.Phonenumber;
            donhang.Totalcost = 0;
            _context.Donhangs.Add(donhang);
            await _context.SaveChangesAsync();
            int newOrderId = donhang.Orderid;
            int total = 0;

            foreach (var product in list)
            {
                var orderProduct = new SamphamDonhang()
                {
                    Orderid=newOrderId,
                    Productid=product.ProductId,
                    Color=product.Color,
                    Quantity= product.Quantity,
                    Cost=product.Price,
                };
                _context.SamphamDonhangs.Add(orderProduct);
                total += product.Quantity * product.Price;
                var khohang = await _context.Khohangs.FindAsync(product.ProductId);
                khohang.Quantity -= product.Quantity; 
            }
            await _context.SaveChangesAsync();
            donhang.Totalcost=total+ship;
            donhang.Status = "đang chờ xác nhận";
            donhang.Time = DateTime.Now;
            await _context.SaveChangesAsync();
            var thongbao = new Thongbao();
            thongbao.Orderid = newOrderId;
            thongbao.Message = user.Name + " vừa đặt đơn hàng " + newOrderId;
            thongbao.Time = DateTime.Now;
            thongbao.Type = "1";
            if (_context.Thongbaos != null)
            {
                _context.Thongbaos.Add(thongbao);

                await _context.SaveChangesAsync();

            }
            return Ok(new { message="Bạn đã đặt hàng thành công",success=true });
        }
        [HttpPost("OrderField")]
        public async Task<ActionResult<Donhang>> PostDonhangField(SanbongDonhang fieldorder, string phone)
        {
            if (_context.Donhangs == null)
            {
                return Problem("Entity set 'ProjectBongDaContext.Donhangs'  is null.");
            }
            var user = await _context.Users.FindAsync(phone);
            if (user == null)
            {
                return NotFound();
            }
            var searchFieldOrder= await _context.SanbongDonhangs.FirstOrDefaultAsync(s=>s.Kip == fieldorder.Kip && s.Times.Date == fieldorder.Times.Date && fieldorder.Fieldid==s.Fieldid);
            if (searchFieldOrder != null)
            {
                return StatusCode(201, new
                {
                    Success = false,
                    Message = "Đã có người đặt kíp này rồi, bạn thử kíp khác xem"
                });
            }
            try
            {
                var donHang = new Donhang();
                donHang.Orderid = 0;
                donHang.Phonenumber = phone;
                donHang.Totalcost = fieldorder.Cost;
                donHang.Time = DateTime.Now;
                donHang.Status = "Đặt thành công, chưa thanh toán";
                _context.Donhangs.Add(donHang);
                await _context.SaveChangesAsync();
                var sanBongDonHang = new SanbongDonhang();
                var newid = donHang.Orderid;
                sanBongDonHang.Orderid = newid;
                sanBongDonHang.Fieldid = fieldorder.Fieldid;
                sanBongDonHang.Kip = fieldorder.Kip;
                sanBongDonHang.Times = fieldorder.Times;
                sanBongDonHang.Cost = fieldorder.Cost;
                sanBongDonHang.Note = fieldorder.Note;
                _context.SanbongDonhangs.Add(sanBongDonHang);
                await _context.SaveChangesAsync();
                var thongbao = new Thongbao();
                thongbao.Orderid = newid; 
                thongbao.Message = user.Name + " vừa đặt sân " + fieldorder.Fieldid + " kíp "+fieldorder.Kip+" ngày "+fieldorder.Times.Date;
                thongbao.Time = DateTime.Now;
                thongbao.Type = "1";
                if (_context.Thongbaos != null)
                {
                    _context.Thongbaos.Add(thongbao);

                    await _context.SaveChangesAsync();

                }
            }
            catch(Exception ex)
            {
                
                return StatusCode(202, new
                {
                    Message = ex.Message+ ex.InnerException,
                    Success = false,
                  //  Message = "Đặt thất bại!"
                });
            }
            
            return Ok(new { Success = true, message = "Bạn đã đặt sân thành công!" });
        }
            // DELETE: api/Donhangs/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonhang(int id)
        {
            if (_context.Donhangs == null)
            {
                return NotFound();
            }
            var donhang = await _context.Donhangs.FindAsync(id);
            if (donhang == null)
            {
                return NotFound();
            }

            _context.Donhangs.Remove(donhang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonhangExists(int id)
        {
            return (_context.Donhangs?.Any(e => e.Orderid == id)).GetValueOrDefault();
        }
    }
}
