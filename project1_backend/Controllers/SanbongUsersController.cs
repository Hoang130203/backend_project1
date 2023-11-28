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
    public class SanbongUsersController : ControllerBase
    {
        private readonly ProjectBongDaContext _context;

        public SanbongUsersController(ProjectBongDaContext context)
        {
            _context = context;
        }

        // GET: api/SanbongUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanbongUser>>> GetSanbongUsers()
        {
          if (_context.SanbongUsers == null)
          {
              return NotFound();
          }
            return await _context.SanbongUsers.ToListAsync();
        }

        // GET: api/SanbongUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SanbongUser>> GetSanbongUser(string id)
        {
          if (_context.SanbongUsers == null)
          {
              return NotFound();
          }
            var sanbongUser = await _context.SanbongUsers.FindAsync(id);

            if (sanbongUser == null)
            {
                return NotFound();
            }

            return sanbongUser;
        }

        // PUT: api/SanbongUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanbongUser(string id, SanbongUser sanbongUser)
        {
            if (id != sanbongUser.Fieldid)
            {
                return BadRequest();
            }

            _context.Entry(sanbongUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanbongUserExists(id))
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

        // POST: api/SanbongUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SanbongUser>> PostSanbongUser(SanbongUser sanbongUser)
        {
          if (_context.SanbongUsers == null)
          {
              return Problem("Entity set 'ProjectBongDaContext.SanbongUsers'  is null.");
          }
            _context.SanbongUsers.Add(sanbongUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SanbongUserExists(sanbongUser.Fieldid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSanbongUser", new { id = sanbongUser.Fieldid }, sanbongUser);
        }

        // DELETE: api/SanbongUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanbongUser(string id)
        {
            if (_context.SanbongUsers == null)
            {
                return NotFound();
            }
            var sanbongUser = await _context.SanbongUsers.FindAsync(id);
            if (sanbongUser == null)
            {
                return NotFound();
            }

            _context.SanbongUsers.Remove(sanbongUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SanbongUserExists(string id)
        {
            return (_context.SanbongUsers?.Any(e => e.Fieldid == id)).GetValueOrDefault();
        }
    }
}
