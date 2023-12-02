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
    public class ProductsController : ControllerBase
    {
        private readonly ProjectBongDaContext _context;

        public ProductsController(ProjectBongDaContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            return await _context.Products.OrderBy(x => Guid.NewGuid()).ToListAsync();
        }
        [HttpGet("AtoZ")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.OrderBy(p=>p.Productname).ToListAsync();
        }
        [HttpGet("ZtoA")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByNameDesc()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.OrderByDescending(p => p.Productname).ToListAsync();
        }
        [HttpGet("Newer")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByNew()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.OrderByDescending(p => p.Productid).ToListAsync();
        }
        [HttpGet("Older")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByOld()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.OrderBy(p => p.Productid).ToListAsync();
        }
        [HttpGet("Cheaper")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCost()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.OrderBy(p => p.Price).ToListAsync();
        }
        [HttpGet("Expensive")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByExpensive()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.OrderByDescending(p => p.Price).ToListAsync();
        }
        [HttpGet("top3")]
        public async Task<ActionResult<IEnumerable<Product>>> GetTopNew()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.OrderByDescending(p => p.Productid).Take(3).ToListAsync();
        }

        //Get by type
        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsType(string type)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.Where(p=>p.Type.Trim().ToLower()==type.Trim().ToLower()).OrderBy(x => Guid.NewGuid()).ToListAsync();
        }
        
        [HttpGet("{type}/AtoZ")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductofTypeByName(string type)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.Where(p => p.Type.Trim().ToLower() == type.Trim().ToLower()).OrderBy(p => p.Productname).ToListAsync();
        }
        [HttpGet("{type}/ZtoA")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductofTypeByNameDesc(string type)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.Where(p => p.Type.Trim().ToLower() == type.Trim().ToLower()).OrderBy(p => p.Productname).OrderByDescending(p => p.Productname).ToListAsync();
        }
              
        [HttpGet("{type}/Newer")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductofTypeByNew(string type)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.Where(p => p.Type.Trim().ToLower() == type.Trim().ToLower()).OrderBy(p => p.Productname).OrderByDescending(p => p.Productid).ToListAsync();
        } 
        [HttpGet("{type}/Older")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductofTypeByOld(string type)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.Where(p => p.Type.Trim().ToLower() == type.Trim().ToLower()).OrderBy(p => p.Productname).OrderBy(p => p.Productid).ToListAsync();
        }
        [HttpGet("{type}/Cheaper")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductofTypeByCost(string type)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.Where(p => p.Type.Trim().ToLower() == type.Trim().ToLower()).OrderBy(p => p.Productname).OrderBy(p => p.Price).ToListAsync();
        }
        [HttpGet("{type}/Expensive")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductofTypeByExpensive(string type)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.Where(p => p.Type.Trim().ToLower() == type.Trim().ToLower()).OrderBy(p => p.Productname).OrderByDescending(p => p.Price).ToListAsync();
        }
        [HttpGet("{type}/top3")]
        public async Task<ActionResult<IEnumerable<Product>>> GetTopOfTypeNew(string type)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.Where(p => p.Type.Trim().ToLower() == type.Trim().ToLower()).OrderBy(p => p.Productname).OrderByDescending(p => p.Productid).Take(3).ToListAsync();
        }
        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Productid)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            var khohang = await _context.Khohangs.FirstOrDefaultAsync(k => k.Productid == id);
            if (khohang != null)
            {
                _context.Khohangs.Remove(khohang);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Productid == id)).GetValueOrDefault();
        }
    }
}
