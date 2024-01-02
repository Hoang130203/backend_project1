using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project1_backend.Models;
using System.Runtime.Serialization;

namespace project1_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongkesController : ControllerBase
    {

        private readonly ProjectBongDaContext _context;
        public ThongkesController(ProjectBongDaContext context)
        {
            _context = context;
        }
        [HttpGet("XepHangNguoiDung")]
        public async Task<ActionResult<IEnumerable<dynamic>>> Xephang(){
            var users= await _context.Users.ToListAsync();
            if (users == null || users.Count == 0)
            {
                return NoContent();
            }
            var bxh= new List<dynamic>();
            foreach (var user in users)
            {
                int totalMoney= await _context.Donhangs.Where(d=>d.Phonenumber==user.Phonenumber).SumAsync(p=>p.Totalcost);
                bxh.Add(new
                {
                    Name = user.Name,
                    Address = user.Address,
                    Phone = user.Phonenumber,
                    Spent = totalMoney
                });
            }
            return bxh.OrderByDescending(b=>b.Spent).ToList();
        }
        [HttpGet("Doanhso1W")]
        public async Task<ActionResult<IEnumerable<object>>>GetDoanhSo()
        {
            if (_context.Donhangs == null)
            {
                return NoContent();
            }
            var doanhsos= await _context.Donhangs.ToListAsync();
            // Lấy ngày hiện tại
            DateTime currentDate = DateTime.Now;

            // Lấy ngày cụ thể cách đây 7 ngày
            DateTime sevenDaysBefore = currentDate.AddDays(-7);

            // Lấy danh sách các ngày trong khoảng 7 ngày gần đây
            List<DateTime> dateRange = Enumerable.Range(0, 7)
                .Select(i => sevenDaysBefore.AddDays(i))
                .ToList();

            // Khởi tạo mảng để lưu tổng tiền của mỗi ngày
            decimal[] totalCosts = new decimal[7];
         //   decimal[] totalCosts1 = new decimal[7];
            var listdoanhso =new List<object>();
            // Lặp qua từng ngày trong danh sách và tính tổng chi phí của các sản phẩm trong đơn hàng trong mỗi ngày
            for (int i = 0; i < 7; i++)
            {
                DateTime specificDate = dateRange[i];
                DayOfWeek dayOfWeekEnum = specificDate.DayOfWeek;
                decimal totalCost = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid 
                    &&dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    &&dh.Status=="Hoàn thành"))
                    .Sum(sp => sp.Cost);
                decimal totalCostGiay = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p=>p.Productid==sp.Productid
                    && p.Type.Trim()== "giày"))
                    .Sum(sp => sp.Cost);
                decimal totalCostQuanAo = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                    && p.Type.Trim() == "quần áo"))
                    .Sum(sp => sp.Cost);
                decimal totalCostBong = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                    && p.Type.Trim() == "bóng"))
                    .Sum(sp => sp.Cost);
                decimal totalCostDungCu = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                    && p.Type.Trim() == "dụng cụ"))
                    .Sum(sp => sp.Cost);
                decimal totalCostPhuKien = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                    && p.Type.Trim() == "phụ kiện"))
                    .Sum(sp => sp.Cost);
                decimal totalCost2 = _context.SanbongDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    ))
                    .Sum(sp => sp.Cost);
                totalCosts[i] = totalCost;
                listdoanhso.Add(new 
                { 
                    day= specificDate.Day+"-"+specificDate.Month+"-"+specificDate.Year,
                    dayOfWeek= (dayOfWeekEnum == DayOfWeek.Sunday) ? "Chủ nhật" : ((int)dayOfWeekEnum + 1).ToString(),
                    totalProduct = totalCost,
                    totalField= totalCost2,
                    totalGiay= totalCostGiay,
                    totalQuanAo=totalCostQuanAo,
                    totalBong=totalCostBong,
                    totalDungCu=totalCostDungCu,
                    totalPhuKien=totalCostPhuKien,
                }
                );
            }

            return  listdoanhso;
        }
        [HttpGet("Doanhso1Month")]
        public async Task<ActionResult<IEnumerable<object>>> GetDoanhSo1Month()
        {
            if (_context.Donhangs == null)
            {
                return NotFound();
            }
            var listdoanhso=new List<object>();
            var listdh = await _context.Donhangs.ToListAsync();
            DateTime currentDate = DateTime.Now;

            // Lấy ngày cụ thể cách đây 7 ngày
            DateTime sevenDaysBefore = currentDate.AddDays(-30);

            // Lấy danh sách các ngày trong khoảng 7 ngày gần đây
            List<DateTime> dateRange = Enumerable.Range(0, 30)
                .Select(i => sevenDaysBefore.AddDays(i))
                .ToList();

            // Khởi tạo mảng để lưu tổng tiền của mỗi ngày
            decimal[] totalCosts = new decimal[30];
            //   decimal[] totalCosts1 = new decimal[7];
            // Lặp qua từng ngày trong danh sách và tính tổng chi phí của các sản phẩm trong đơn hàng trong mỗi ngày
            for (int i = 0; i < 30; i++)
            {
                DateTime specificDate = dateRange[i];
                DayOfWeek dayOfWeekEnum = specificDate.DayOfWeek;
                decimal totalCost = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    && dh.Status == "Hoàn thành"))
                    .Sum(sp => sp.Cost);
                decimal totalCostGiay = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                    && p.Type.Trim() == "giày"))
                    .Sum(sp => sp.Cost);
                decimal totalCostQuanAo = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                    && p.Type.Trim() == "quần áo"))
                    .Sum(sp => sp.Cost);
                decimal totalCostBong = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                    && p.Type.Trim() == "bóng"))
                    .Sum(sp => sp.Cost);
                decimal totalCostDungCu = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                    && p.Type.Trim() == "dụng cụ"))
                    .Sum(sp => sp.Cost);
                decimal totalCostPhuKien = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                    && p.Type.Trim() == "phụ kiện"))
                    .Sum(sp => sp.Cost);
                decimal totalCost2 = _context.SanbongDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Date == specificDate.Date
                    ))
                    .Sum(sp => sp.Cost);
                totalCosts[i] = totalCost;
                listdoanhso.Add(new
                {
                    day = specificDate.Day + "-" + specificDate.Month + "-" + specificDate.Year,
                    dayOfWeek = (dayOfWeekEnum == DayOfWeek.Sunday) ? "Chủ nhật" : ((int)dayOfWeekEnum + 1).ToString(),
                    totalProduct = totalCost,
                    totalField = totalCost2,
                    totalGiay = totalCostGiay,
                    totalQuanAo = totalCostQuanAo,
                    totalBong = totalCostBong,
                    totalDungCu = totalCostDungCu,
                    totalPhuKien = totalCostPhuKien,
                }
                );
            }
            return listdoanhso;
        }
        [HttpGet("DoanhSo1Y")]
        public async Task<ActionResult<IEnumerable<object>>> Getdoanhso1Y(int Year)
        {
            if (_context.Donhangs == null)
            {
                return NotFound();
            }
            var list= await _context.Donhangs.ToListAsync();
            var listdoanhso = new List<object>();
            DateTime currentDate= DateTime.Now; 
            for (int i = 1; i <= 12; i++)
            {
                decimal totalCost = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Month == i && dh.Time.Value.Year==Year
                    && dh.Status == "Hoàn thành" ) && _context.SamphamDonhangs.Where(p => p.Orderid == sp.Orderid).ToList().Count!=0)
                    .Sum(sp => sp.Cost);
                decimal totalCostGiay = _context.SamphamDonhangs
                   .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                   && dh.Time.HasValue && dh.Time.Value.Month == i && dh.Time.Value.Year == Year
                   && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                   && p.Type.Trim() == "giày"))
                   .Sum(sp => sp.Cost);
                decimal totalCostQuanAo = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Month == i && dh.Time.Value.Year == Year
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                    && p.Type.Trim() == "quần áo"))
                    .Sum(sp => sp.Cost);
                decimal totalCostBong = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Month == i && dh.Time.Value.Year == Year
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                    && p.Type.Trim() == "bóng"))
                    .Sum(sp => sp.Cost);
                decimal totalCostDungCu = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Month == i && dh.Time.Value.Year == Year
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                    && p.Type.Trim() == "dụng cụ"))
                    .Sum(sp => sp.Cost);
                decimal totalCostPhuKien = _context.SamphamDonhangs
                    .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                    && dh.Time.HasValue && dh.Time.Value.Month == i && dh.Time.Value.Year == Year
                    && dh.Status == "Hoàn thành") && _context.Products.Any(p => p.Productid == sp.Productid
                    && p.Type.Trim() == "phụ kiện"))
                    .Sum(sp => sp.Cost);
                decimal totalCost2 = _context.SanbongDonhangs
                  .Where(sp => _context.Donhangs.Any(dh => dh.Orderid == sp.Orderid
                  && dh.Time.HasValue && dh.Time.Value.Month == i && dh.Time.Value.Year==Year
                  )&& _context.SamphamDonhangs.Where(p => p.Orderid == sp.Orderid).ToList().Count == 0)
                  .Sum(sp => sp.Cost);
                listdoanhso.Add(new
                {
                    Month = "Tháng "+i,
                    totalProduct = totalCost,
                    totalField= totalCost2,
                    totalGiay = totalCostGiay,
                    totalQuanAo = totalCostQuanAo,
                    totalBong = totalCostBong,
                    totalDungCu = totalCostDungCu,
                    totalPhuKien = totalCostPhuKien,
                });
            }
            return listdoanhso;
        }
  
    }
}
