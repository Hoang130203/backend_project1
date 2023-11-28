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

        // POST: api/Khohangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Khohang>> PostKhohang(Khohang khohang)
        {
          if (_context.Khohangs == null)
          {
              return Problem("Entity set 'ProjectBongDaContext.Khohangs'  is null.");
          }
            _context.Khohangs.Add(khohang);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KhohangExists(khohang.Productid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKhohang", new { id = khohang.Productid }, khohang);
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
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhohangExists(int id)
        {
            return (_context.Khohangs?.Any(e => e.Productid == id)).GetValueOrDefault();
        }
    }
}
