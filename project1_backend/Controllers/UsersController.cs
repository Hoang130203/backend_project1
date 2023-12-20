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
        public async Task<ActionResult<object>> GetUser(string id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);
            var account= await _context.Accounts.FindAsync(id);
            if (user == null ||account==null)
            {
                return NotFound();
            }
            var obj = new
            {
                phonenumber = user.Phonenumber,
                name = user.Name,
                birthdate = user.Birthdate,
                gender = user.Gender,
                address = user.Address,
                avt = user.Avt,
                password = account.Password
            };
            return obj;
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
        [HttpPut("avatar")]
        public async Task<IActionResult> PutAvatar(string url,string phone)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user= await _context.Users.FirstOrDefaultAsync(u=>u.Phonenumber==phone);
            if (user != null)
            {
                user.Avt = url;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.Phonenumber == id)).GetValueOrDefault();
        }
    }
}
