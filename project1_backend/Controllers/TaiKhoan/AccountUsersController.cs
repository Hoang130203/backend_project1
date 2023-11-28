using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using project1_backend.Models;
using project1_backend.Models.Custom;

namespace project1_backend.Controllers.TaiKhoan
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountUsersController : ControllerBase
    {
        private readonly ProjectBongDaContext _context;

        public AccountUsersController(ProjectBongDaContext context)
        {
            _context = context;
        }

        // GET: api/AccountUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAccountUser()
        {
          
          if (_context.Accounts == null)
          {
              return NotFound();

          }
            var userAccounts = new List<object>();
            var users = await _context.Users.AsNoTracking().ToListAsync();
            foreach (var user in users)
            {
                var phone = user.Phonenumber;
                var account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Phonenumber == phone);
                var useraccount = new
                {
                    user.Phonenumber,
                    user.Name,
                    user.Birthdate,
                    user.Avt,
                    user.Address,
                    password = account?.Password
                };
                userAccounts.Add(useraccount);
            }
            return userAccounts;
        }

        // GET: api/AccountUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountUser>> GetAccountUser(string id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Phonenumber == id);
            var user=await _context.Users.FirstOrDefaultAsync(b=>b.Phonenumber == id);
            if (account == null || user == null)
            {
                return Problem("tài khoản không tồn tại");
            }
            var accountUser = new AccountUser()
            {
                PhoneNumber = id,
                Name = user.Name,
                PassWord=account.Password,
                Address=user.Address??"",
            };

            return accountUser;
        }

        // PUT: api/AccountUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountUser(string id, AccountUser accountUser)
        {
            if (id != accountUser.PhoneNumber)
            {
                return BadRequest();
            }
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Phonenumber == id);
            var user = await _context.Users.FirstOrDefaultAsync(b => b.Phonenumber == id);
            account.Password = accountUser.PassWord;
            user.Address= accountUser.Address;
            user.Name = accountUser.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(201, new
            {
                Success = true,
                Message = "access"
            });
        }
        [HttpPut("avt/{id}")]
        public async Task<IActionResult> PutAvtUser(string id, string avt)
        {
            var user= await _context.Users.FirstOrDefaultAsync(user=>user.Phonenumber==id);
            if (user == null)
            {
                return NotFound();
            }
            user.Avt = avt; 
            await _context.SaveChangesAsync();
            return StatusCode(201, new
            {
                Success = true,
                Message = "access"
            });
        }
        // POST: api/AccountUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        public async Task<ActionResult<AccountUser>> PostAccountUser(AccountUser accountUser)
        {
          if (_context.AccountUser == null)
          {
              return Problem("Entity set 'ProjectBongDaContext.AccountUser'  is null.");
            }
            var account = await _context.Accounts.FirstOrDefaultAsync(p => p.Phonenumber == accountUser.PhoneNumber);
            if(account != null)
            {
                return Problem("tai khoan da ton tai");

            }
            var user = new User()
            {
                Phonenumber = accountUser.PhoneNumber,
                Name= accountUser.Name,
                Address = accountUser.Address,
            };
            

            var account1 = new Account()
            {
                Phonenumber = accountUser.PhoneNumber,
                Password = accountUser.PassWord,
            };
            _context.Users.Add(user);
            _context.Accounts.Add(account1);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountUserExists(accountUser.PhoneNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccountUser", new { id = accountUser.PhoneNumber }, accountUser);
        }

        // DELETE: api/AccountUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountUser(string id)
        {
            if (_context.AccountUser == null)
            {
                return NotFound();
            }
            var accountUser = await _context.AccountUser.FindAsync(id);
            if (accountUser == null)
            {
                return NotFound();
            }

            _context.AccountUser.Remove(accountUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountUserExists(string id)
        {
            return (_context.AccountUser?.Any(e => e.PhoneNumber == id)).GetValueOrDefault();
        }
    }
}
