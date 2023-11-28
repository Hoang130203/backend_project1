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
    public class SamphamDonhangsController : ControllerBase
    {
        private readonly ProjectBongDaContext _context;

        public SamphamDonhangsController(ProjectBongDaContext context)
        {
            _context = context;
        }

        // GET: api/SamphamDonhangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SamphamDonhang>>> GetSamphamDonhangs()
        {
          if (_context.SamphamDonhangs == null)
          {
              return NotFound();
          }
            return await _context.SamphamDonhangs.ToListAsync();
        }

        // GET: api/SamphamDonhangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SamphamDonhang>> GetSamphamDonhang(int id)
        {
          if (_context.SamphamDonhangs == null)
          {
              return NotFound();
          }
            var samphamDonhang = await _context.SamphamDonhangs.FindAsync(id);

            if (samphamDonhang == null)
            {
                return NotFound();
            }

            return samphamDonhang;
        }

        // PUT: api/SamphamDonhangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSamphamDonhang(int id, SamphamDonhang samphamDonhang)
        {
            if (id != samphamDonhang.Orderid)
            {
                return BadRequest();
            }

            _context.Entry(samphamDonhang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SamphamDonhangExists(id))
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

        // POST: api/SamphamDonhangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SamphamDonhang>> PostSamphamDonhang(SamphamDonhang samphamDonhang)
        {
          if (_context.SamphamDonhangs == null)
          {
              return Problem("Entity set 'ProjectBongDaContext.SamphamDonhangs'  is null.");
          }
            _context.SamphamDonhangs.Add(samphamDonhang);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SamphamDonhangExists(samphamDonhang.Orderid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSamphamDonhang", new { id = samphamDonhang.Orderid }, samphamDonhang);
        }

        // DELETE: api/SamphamDonhangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSamphamDonhang(int id)
        {
            if (_context.SamphamDonhangs == null)
            {
                return NotFound();
            }
            var samphamDonhang = await _context.SamphamDonhangs.FindAsync(id);
            if (samphamDonhang == null)
            {
                return NotFound();
            }

            _context.SamphamDonhangs.Remove(samphamDonhang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SamphamDonhangExists(int id)
        {
            return (_context.SamphamDonhangs?.Any(e => e.Orderid == id)).GetValueOrDefault();
        }
    }
}
