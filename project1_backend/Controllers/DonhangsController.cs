using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project1_backend.Models;
using project1_backend.Models.Custom;
using project1_backend.Service;

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

        // GET: api/Donhangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donhang>>> GetDonhangs()
        {
          if (_context.Donhangs == null)
          {
              return NotFound();
          }
            return await _context.Donhangs.ToListAsync();
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
        [HttpGet("allofUser")]
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
                var sanpham = await _context.SamphamDonhangs.Where(s => s.Orderid == spdh.Orderid).ToListAsync();
                var chitiet= new List<object>();
                foreach (var sp in sanpham)
                {
                    chitiet.Add(await _context.Products.FindAsync(sp.Productid));

                }
                
                var donhangWithSanpham = new { Donhang = spdh, Sanpham = sanpham,Chitiet=chitiet };
                list.Add(donhangWithSanpham);
            }
            if (user == null)
            {
                return NotFound();
            }

            return list;
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

        // POST: api/Donhangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Donhang>> PostDonhang(List<InfoProduct> list,string phone,bool isSanbong)
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
            var donhang=new Donhang();
            donhang.Orderid = 0;
            donhang.Phonenumber=user.Phonenumber;
            donhang.Totalcost = 0;
            _context.Donhangs.Add(donhang);
            await _context.SaveChangesAsync();
            int newOrderId = donhang.Orderid;
            int total = 0;
            if (isSanbong)
            {

            }
            else {
                foreach (var product in list)
                {
                    var orderProduct = new SamphamDonhang()
                    {
                       Orderid=newOrderId,
                       Productid=product.ProductId,
                       Quantity= product.Quantity,
                       Cost=product.Price,
                    };
                    _context.SamphamDonhangs.Add(orderProduct);
                    total += product.Quantity * product.Price;
                }
                await _context.SaveChangesAsync();

                donhang.Totalcost=total;
                donhang.Status = "đang chờ xác nhận";
                await _context.SaveChangesAsync();
                var thongbao = new Thongbao();
                thongbao.Orderid = newOrderId;
                thongbao.Message = user.Name + " vừa đặt đơn hàng " + newOrderId;
                thongbao.Time= DateTime.Now;
                thongbao.Type = "1";
                var createthongbao = new CreateThongBao(_context);
                createthongbao.create(thongbao);

            }


            return Ok(new { message="you ordered" });
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
