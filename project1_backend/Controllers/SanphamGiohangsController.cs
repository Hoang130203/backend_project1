using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using project1_backend.Models;
using project1_backend.Models.Custom;

namespace project1_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanphamGiohangsController : ControllerBase
    {
        private readonly ProjectBongDaContext _context;

        public SanphamGiohangsController(ProjectBongDaContext context)
        {
            _context = context;
        }

        // GET: api/SanphamGiohangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanphamGiohang>>> GetSanphamGiohangs()
        {
          if (_context.SanphamGiohangs == null)
          {
              return NotFound();
          }
            return await _context.SanphamGiohangs.ToListAsync();
        }

        // GET: api/SanphamGiohangs/5
        [HttpGet("ByID")]
        public async Task<ActionResult<SanphamGiohang>> GetSanphamGiohang(string phone,int productid)
        {
          if (_context.SanphamGiohangs == null)
          {
              return NotFound();
          }
            var sanphamGiohang = await _context.SanphamGiohangs.FindAsync(phone,productid);

            if (sanphamGiohang == null)
            {
                return NotFound();
            }

            return sanphamGiohang;
        }
        [HttpGet("ByPhone")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllByPhone(string phone)
        {
            if (_context.SanphamGiohangs == null)
            {
                return NotFound();
            }
            var sanphamGiohang = await _context.SanphamGiohangs.Where(s=>s.Userphonenumber == phone).ToListAsync();
            var infogiohang =new List<object>();
            foreach(var i in sanphamGiohang)
            {
                var product = await _context.Products.FindAsync(i.Productid);
                var info = new InfoProduct();
                info.ProductId = i.Productid;
                info.Linkimg = product?.Linkimg;
                info.ProductName=product?.Productname;
                info.Quantity = i.Quantity;
                info.Price = i.Price;
                info.Color = i.Color;
                infogiohang.Add(info);
            }
            if (sanphamGiohang == null)
            {
                return NotFound();
            }

            return infogiohang;
        }

        // PUT: api/SanphamGiohangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("updateCart")]
        public async Task<IActionResult> PutSanphamGiohang(SanphamGiohang sanphamGiohang)
        {
            

            _context.Entry(sanphamGiohang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanphamGiohangExists(sanphamGiohang.Userphonenumber,sanphamGiohang.Productid, sanphamGiohang.Color))
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

        // POST: api/SanphamGiohangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SanphamGiohang>> PostSanphamGiohang(SanphamGiohang sanphamGiohang)
        {
          if (_context.SanphamGiohangs == null)
          {
              return Problem("Entity set 'ProjectBongDaContext.SanphamGiohangs'  is null.");
            }
            var user = await _context.Users.FindAsync(sanphamGiohang.Userphonenumber);
            var product = await _context.Products.FindAsync(sanphamGiohang.Productid);
            if (user == null || product == null)
            {
                return Ok(new { success = false });
            }
            var spgh = await _context.SanphamGiohangs.FindAsync(sanphamGiohang.Userphonenumber, sanphamGiohang.Productid,sanphamGiohang.Color);
            if (spgh == null )
            {
                _context.SanphamGiohangs.Add(sanphamGiohang);
            }
            else
            {
                spgh.Quantity += sanphamGiohang.Quantity;
                spgh.Price = sanphamGiohang.Price;
            }
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new {success=true});
            }
            catch (DbUpdateException)
            {
                if (SanphamGiohangExists(sanphamGiohang.Userphonenumber,sanphamGiohang.Productid,sanphamGiohang.Color))
                {
                    return Ok(new { success = false });
                }
                else
                {
                    throw;
                }
            }
           

        }

        // DELETE: api/SanphamGiohangs/5
        [HttpDelete]
        public async Task<IActionResult> DeleteSanphamGiohang(string phone,int productid,string color)
        {
            if (_context.SanphamGiohangs == null)
            {
                return NotFound();
            }
            var sanphamGiohang = await _context.SanphamGiohangs.FindAsync(phone,productid, color.Replace(".", ""));
            if (sanphamGiohang == null)
            {
                return NotFound();
            }

            _context.SanphamGiohangs.Remove(sanphamGiohang);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllGioHang(string phone)
        {
            if (_context.SanphamGiohangs == null)
            {
                return NotFound();
            }
            var sanphamgiohang=await _context.SanphamGiohangs.Where(s=>s.Userphonenumber==phone).ToListAsync();
            if (sanphamgiohang == null)
            {
                return NotFound();
            }
            foreach (var item in sanphamgiohang)
            {
                _context.SanphamGiohangs.Remove(item);

            }
            await _context.SaveChangesAsync();
            return Ok(new { message = "deleted" });
        }
        private bool SanphamGiohangExists(string id1,int id2,string id3)
        {
            return (_context.SanphamGiohangs?.Any(e => e.Userphonenumber == id1 &&e.Productid==id2 && e.Color==id3)).GetValueOrDefault();
        }
    }
}
