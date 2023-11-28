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
    public class ProductUsersController : ControllerBase
    {
        private readonly ProjectBongDaContext _context;

        public ProductUsersController(ProjectBongDaContext context)
        {
            _context = context;
        }

        // GET: api/ProductUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductUser>>> GetProductUsers()
        {
          if (_context.ProductUsers == null)
          {
              return NotFound();
          }
            return await _context.ProductUsers.ToListAsync();
        }

        // GET: api/ProductUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductUser>> GetProductUser(int id)
        {
          if (_context.ProductUsers == null)
          {
              return NotFound();
          }
            var productUser = await _context.ProductUsers.FindAsync(id);

            if (productUser == null)
            {
                return NotFound();
            }

            return productUser;
        }

        // PUT: api/ProductUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductUser(int id, ProductUser productUser)
        {
            if (id != productUser.Productid)
            {
                return BadRequest();
            }

            _context.Entry(productUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductUserExists(id))
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

        // POST: api/ProductUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductUser>> PostProductUser(ProductUser productUser)
        {
          if (_context.ProductUsers == null)
          {
              return Problem("Entity set 'ProjectBongDaContext.ProductUsers'  is null.");
          }
            _context.ProductUsers.Add(productUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductUserExists(productUser.Productid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductUser", new { id = productUser.Productid }, productUser);
        }

        // DELETE: api/ProductUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductUser(int id)
        {
            if (_context.ProductUsers == null)
            {
                return NotFound();
            }
            var productUser = await _context.ProductUsers.FindAsync(id);
            if (productUser == null)
            {
                return NotFound();
            }

            _context.ProductUsers.Remove(productUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductUserExists(int id)
        {
            return (_context.ProductUsers?.Any(e => e.Productid == id)).GetValueOrDefault();
        }
    }
}
