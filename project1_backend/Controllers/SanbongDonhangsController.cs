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
    public class SanbongDonhangsController : ControllerBase
    {
        private readonly ProjectBongDaContext _context;

        public SanbongDonhangsController(ProjectBongDaContext context)
        {
            _context = context;
        }

        // GET: api/SanbongDonhangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanbongDonhang>>> GetSanbongDonhangs()
        {
          if (_context.SanbongDonhangs == null)
          {
              return NotFound();
          }
            return await _context.SanbongDonhangs.ToListAsync();
        }

        // GET: api/SanbongDonhangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SanbongDonhang>> GetSanbongDonhang(int id)
        {
          if (_context.SanbongDonhangs == null)
          {
              return NotFound();
          }
            var sanbongDonhang = await _context.SanbongDonhangs.FindAsync(id);

            if (sanbongDonhang == null)
            {
                return NotFound();
            }

            return sanbongDonhang;
        }

        // PUT: api/SanbongDonhangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanbongDonhang(int id, SanbongDonhang sanbongDonhang)
        {
            if (id != sanbongDonhang.Orderid)
            {
                return BadRequest();
            }

            _context.Entry(sanbongDonhang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanbongDonhangExists(id))
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

        // POST: api/SanbongDonhangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SanbongDonhang>> PostSanbongDonhang(SanbongDonhang sanbongDonhang)
        {
          if (_context.SanbongDonhangs == null)
          {
              return Problem("Entity set 'ProjectBongDaContext.SanbongDonhangs'  is null.");
          }
            _context.SanbongDonhangs.Add(sanbongDonhang);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SanbongDonhangExists(sanbongDonhang.Orderid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSanbongDonhang", new { id = sanbongDonhang.Orderid }, sanbongDonhang);
        }

        // DELETE: api/SanbongDonhangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanbongDonhang(int id)
        {
            if (_context.SanbongDonhangs == null)
            {
                return NotFound();
            }
            var sanbongDonhang = await _context.SanbongDonhangs.FindAsync(id);
            if (sanbongDonhang == null)
            {
                return NotFound();
            }

            _context.SanbongDonhangs.Remove(sanbongDonhang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SanbongDonhangExists(int id)
        {
            return (_context.SanbongDonhangs?.Any(e => e.Orderid == id)).GetValueOrDefault();
        }
    }
}
