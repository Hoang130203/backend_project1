using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project1_backend.Models;

namespace project1_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhohangsController : ControllerBase
    {
        private readonly ProjectBongDaContext _context;

        public KhohangsController(ProjectBongDaContext context)
        {
            _context = context;
        }

        // GET: api/Khohangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Khohang>>> GetKhohangs()
        {
          if (_context.Khohangs == null)
          {
              return NotFound();
          }
            return await _context.Khohangs.ToListAsync();
        }

        // GET: api/Khohangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Khohang>> GetKhohang(int id)
        {
          if (_context.Khohangs == null)
          {
              return NotFound();
          }
            var khohang = await _context.Khohangs.FindAsync(id);

            if (khohang == null)
            {
                return NotFound();
            }

            return khohang;
        }

        // PUT: api/Khohangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhohang(int id, Khohang khohang)
        {
            if (id != khohang.Productid)
            {
                return BadRequest();
            }

            _context.Entry(khohang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhohangExists(id))
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


        // DELETE: api/Khohangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhohang(int id)
        {
            if (_context.Khohangs == null)
            {
                return NotFound();
            }
            var khohang = await _context.Khohangs.FindAsync(id);
            if (khohang == null)
            {
                return NotFound();
            }

            _context.Khohangs.Remove(khohang);
            var product= await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);

            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhohangExists(int id)
        {
            return (_context.Khohangs?.Any(e => e.Productid == id)).GetValueOrDefault();
        }
    }
}
