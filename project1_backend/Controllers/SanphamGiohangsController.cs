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
        [HttpGet("{id}")]
        public async Task<ActionResult<SanphamGiohang>> GetSanphamGiohang(string id)
        {
          if (_context.SanphamGiohangs == null)
          {
              return NotFound();
          }
            var sanphamGiohang = await _context.SanphamGiohangs.FindAsync(id);

            if (sanphamGiohang == null)
            {
                return NotFound();
            }

            return sanphamGiohang;
        }

        // PUT: api/SanphamGiohangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanphamGiohang(string id, SanphamGiohang sanphamGiohang)
        {
            if (id != sanphamGiohang.Userphonenumber)
            {
                return BadRequest();
            }

            _context.Entry(sanphamGiohang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanphamGiohangExists(id))
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
            _context.SanphamGiohangs.Add(sanphamGiohang);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SanphamGiohangExists(sanphamGiohang.Userphonenumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSanphamGiohang", new { id = sanphamGiohang.Userphonenumber }, sanphamGiohang);
        }

        // DELETE: api/SanphamGiohangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanphamGiohang(string id)
        {
            if (_context.SanphamGiohangs == null)
            {
                return NotFound();
            }
            var sanphamGiohang = await _context.SanphamGiohangs.FindAsync(id);
            if (sanphamGiohang == null)
            {
                return NotFound();
            }

            _context.SanphamGiohangs.Remove(sanphamGiohang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SanphamGiohangExists(string id)
        {
            return (_context.SanphamGiohangs?.Any(e => e.Userphonenumber == id)).GetValueOrDefault();
        }
    }
}
