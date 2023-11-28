using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using project1_backend.Models;

namespace project1_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ProjectBongDaContext _context;

        public UsersController(ProjectBongDaContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Phonenumber)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        [HttpPost("login")]
        public async Task<ActionResult> Login(string account, string password)
        {
            if (_context.Admins==null && _context.Users==null)
            {
                return NotFound();
            }
            try
            {
                var admin = await _context.Admins.FirstOrDefaultAsync(p => p.Account == account && p.Password == password);
                if (admin != null)
                {
                    return StatusCode(201, new
                    {
                        Success = true,
                        Message = "Admin"
                    });
                }
                var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Phonenumber == account && u.Password == password);
                if (user != null)
                {
                    return StatusCode(201, new
                    {
                        Success = true,
                        Message = "User"
                    });
                }
            }
            catch
            {
                return NotFound();
            }
            return StatusCode(202, new
            {
                Success = false,
                Message = "invalid account or password"
            }) ;
        }
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'ProjectBongDaContext.Users'  is null.");
          }
            var userr= await _context.Users.FirstOrDefaultAsync(p=>p.Phonenumber == user.Phonenumber);
            if (userr != null)
            {
                return Problem("account existed");

            }
            _context.Users.Add(user);
            var account = new Account();
            account.Phonenumber = user.Phonenumber;
            account.Password = "123";
            _context.Accounts.Add(account);

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                if (UserExists(user.Phonenumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Phonenumber }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.Phonenumber == id)).GetValueOrDefault();
        }
    }
}
