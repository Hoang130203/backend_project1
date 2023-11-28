using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project1_backend.Models;

namespace project1_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanbongsController : ControllerBase
    {
        private readonly ProjectBongDaContext _context;

        public SanbongsController(ProjectBongDaContext context)
        {
            _context = context;
        }

        // GET: api/Sanbongs
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Sanbong>>> GetSanbongs()
        {
          if (_context.Sanbongs == null)
          {
              return NotFound();
          }
            return await _context.Sanbongs.ToListAsync();
        }

        // GET: api/Sanbongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sanbong>> GetSanbong(string id)
        {
          if (_context.Sanbongs == null)
          {
              return NotFound();
          }
            var sanbong = await _context.Sanbongs.FindAsync(id);

            if (sanbong == null)
            {
                return NotFound();
            }

            return sanbong;
        }

        // PUT: api/Sanbongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutSanbong(string id, Sanbong sanbong)
        {
            if (id != sanbong.Fieldid)
            {
                return BadRequest();
            }

            _context.Entry(sanbong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { Message="updated" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanbongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


        }

        // POST: api/Sanbongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sanbong>> PostSanbong(Sanbong sanbong)
        {
          if (_context.Sanbongs == null)
          {
              return Problem("Entity set 'ProjectBongDaContext.Sanbongs'  is null.");
          }
            string id = sanbong.Fieldid;
            var sb = await _context.Sanbongs.FindAsync(id);
            while (true)
            {
                if (sb == null)
                {
                    break;
                }
                id= GenerateRandomString(5);
                sb = await _context.Sanbongs.FindAsync(id);
            }
            sanbong.Fieldid = id;
            sanbong.Cost = sanbong.Price;
            _context.Sanbongs.Add(sanbong);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { Message = $"Created Sanbong with ID: {sanbong.Fieldid}",Content=sanbong.Fieldid });
            }
            catch (DbUpdateException)
            {
                if (SanbongExists(sanbong.Fieldid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

        
        }

        // DELETE: api/Sanbongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanbong(string id)
        {
            if (_context.Sanbongs == null)
            {
                return NotFound();
            }
            var sanbong = await _context.Sanbongs.FindAsync(id);
            if (sanbong == null)
            {
                return NotFound();
            }

            _context.Sanbongs.Remove(sanbong);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }

            return stringBuilder.ToString();
        }
        private bool SanbongExists(string id)
        {
            return (_context.Sanbongs?.Any(e => e.Fieldid == id)).GetValueOrDefault();
        }
    }
}
