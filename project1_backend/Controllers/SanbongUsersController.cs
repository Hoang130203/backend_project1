using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
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
        [HttpGet("FindComment")]
        public async Task<ActionResult<object>> GetField_User(string fieldid, string phone, DateTime time)
        {
            if (_context.SanbongUsers == null)
            {
                return NoContent();
            }
            var sanbongUser = await _context.SanbongUsers.FirstOrDefaultAsync(p => p.Fieldid == fieldid
            && p.Userphonenumber == phone && p.Time.Date == time.Date && p.Time.Hour == time.Hour && p.Time.Minute == time.Minute && p.Time.Second == time.Second);

            if (sanbongUser == null)
            {
                return NoContent();
            }

            return sanbongUser;
        }
        //Get all comment
        [HttpGet("GetAllCmtOfField")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllComment(string fieldid)
        {
            if (_context.SanbongUsers    == null)
            {
                return NoContent();
            }
            var sbUsers = await _context.SanbongUsers.Where(p => p.Fieldid == fieldid).OrderByDescending(p => p.Time).ToListAsync();
            var listcomment = new List<object>();
            foreach (var p in sbUsers)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Phonenumber == p.Userphonenumber);
                var obj = new
                {
                    danhgia = p,
                    user = user,
                };
                listcomment.Add(obj);
            }
            if (listcomment == null)
            {
                return NoContent();
            }

            return listcomment;
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
                if (!SanbongUserExists(id,sanbongUser.Userphonenumber,sanbongUser.Time))
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
            try
            {
                  _context.SanbongUsers.Add(sanbongUser);
                await _context.SaveChangesAsync();
                var field_comment = await _context.SanbongUsers.Where(pc => pc.Fieldid == sanbongUser.Fieldid).ToListAsync();
                int rate = 0;
                foreach (var cmt in field_comment)
                {
                    rate += cmt.Rate ?? 0;
                }
                rate = rate /field_comment.Count();
                var f = await _context.Sanbongs.FindAsync(sanbongUser.Fieldid);
                if (f != null)
                {
                   f.Rate = rate;
                }
                await _context.SaveChangesAsync();
                var thongbao = new Thongbao();
                var user = await _context.Users.FindAsync(sanbongUser.Userphonenumber);
                var ftd = await _context.Sanbongs.FirstOrDefaultAsync(o => o.Fieldid == sanbongUser.Fieldid);

                //    Random rd=new Random();
                
                
                thongbao.Orderid = 3;
                thongbao.Message = user?.Name + " vừa đánh giá "+sanbongUser.Rate+" sao sân " +ftd?.Name + " \"" + sanbongUser.Comment + " \"";
                thongbao.Time = DateTime.Now;
                thongbao.Type = "3";
                if (_context.Thongbaos != null)
                {
                    _context.Thongbaos.Add(thongbao);

                    await _context.SaveChangesAsync();

                }
            }
            catch (DbUpdateException)
            {
                if (SanbongUserExists(sanbongUser.Fieldid,sanbongUser.Userphonenumber,sanbongUser.Time))
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
        [HttpDelete]
        public async Task<IActionResult> DeleteSanbongUser(string id, string phone, DateTime date)
        {
            if (_context.SanbongUsers == null)
            {
                return NotFound();
            }
            var sanbongUser = await _context.SanbongUsers.FirstOrDefaultAsync(s=>s.Fieldid==id && s.Userphonenumber==phone && s.Time.Day==date.Day 
                &&s.Time.Hour==date.Hour && s.Time.Minute==date.Minute && s.Time.Second==date.Second &&s.Time.Month==date.Month);
            if (sanbongUser == null)
            {
                return NotFound();
            }

            _context.SanbongUsers.Remove(sanbongUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SanbongUserExists(string id,string phone, DateTime date)
        {
            return (_context.SanbongUsers?.Any(e => e.Fieldid == id && e.Userphonenumber==phone && e.Time.Hour==date.Hour
                &&e.Time.Day==date.Day && e.Time.Minute== date.Minute && e.Time.Second== date.Second)).GetValueOrDefault();
        }
    }
}
