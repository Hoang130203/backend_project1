using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project1_backend.Models;
using project1_backend.Models.Custom;

namespace project1_backend.Controllers.ForAdmin
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoProductsController : ControllerBase
    {
        private readonly ProjectBongDaContext _context;

        public InfoProductsController(ProjectBongDaContext context)
        {
            _context = context;
        }

        // GET: api/InfoProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetInfoProduct()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.ToListAsync();
        }

        [HttpGet("Type")]
        public async Task<ActionResult<IEnumerable<InfoProduct>>> SearchProducts(string Type)
        {
            var products = await _context.Products
                .Where(p =>                 
                    (string.IsNullOrEmpty(Type) || p.Type.ToUpper().Contains(Type.ToUpper())))
                .Select(p => new InfoProduct
                {
                    ProductId=p.Productid,
                    ProductName= p.Productname,
                    Linkimg= p.Linkimg,
                    Price= p.Price,
                    Description=p.Detail,
                    Color= p.Color,
                    Type=p.Type,
                    Quantity = _context.Khohangs.FirstOrDefault(ip => ip.Productid == p.Productid) != null ?
                _context.Khohangs.FirstOrDefault(ip => ip.Productid == p.Productid).Quantity :0
                })
                .ToListAsync();

            return products;
        }

        // GET: api/InfoProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InfoProduct>> GetInfoProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var khohang = await _context.Khohangs.FirstOrDefaultAsync(k => k.Productid == id);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Productid == id);
            if (khohang == null || product == null)
            {
                return Problem("khong ton tai kho hang hoac san pham");

            }

            var infoProduct = new InfoProduct()
            {
                ProductId = id,
                ProductName = product.Productname,
                Price = product.Price,
                Color = product.Color,
                Linkimg = product.Linkimg,
                Description = product.Detail,
                Quantity = khohang.Quantity,
                Type = product.Type,

            };

            if (infoProduct == null)
            {
                return NotFound();
            }

            return infoProduct;
        }

        // PUT: api/InfoProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfoProduct(int id, InfoProduct infoProduct)
        {
            if (_context.Products == null || _context.Khohangs == null)
            {
                return Problem("Entity set 'ProjectBongDaContext.InfoProduct'  is null.");
            }
            var khohang=await _context.Khohangs.FirstOrDefaultAsync(_ => _.Productid == id);
            var product = await _context.Products.FirstOrDefaultAsync(_ => _.Productid == id);
            if (khohang == null || product == null)
            {
                return NotFound();
            }
            khohang.Quantity = infoProduct.Quantity;
            product.Productname = infoProduct.ProductName;
            product.Price = infoProduct.Price;
            product.Type = infoProduct.Type??product.Type;
            product.Detail = infoProduct.Description;
            product.Color = infoProduct.Color??product.Color;
            product.Linkimg=infoProduct.Linkimg??product.Linkimg;
            _context.Entry(khohang).State = EntityState.Modified;
            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfoProductExists(id))
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

        // POST: api/InfoProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InfoProduct>> PostInfoProduct(InfoProduct infoProduct)
        {
            if (_context.Products == null || _context.Khohangs==null)
            {
                return Problem("Entity set 'ProjectBongDaContext.InfoProduct'  is null.");
            }

            var product = new Product();
            product.Productname = infoProduct.ProductName;
            product.Linkimg= infoProduct.Linkimg;
            product.Color = infoProduct.Color??"";
            product.Detail = infoProduct.Description;
            product.Type = infoProduct.Type??"";
            product.Price= infoProduct.Price;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            var maxProductId = await _context.Products.MaxAsync(p => (int?)p.Productid) ?? 0;
            var khohang = new Khohang();
            khohang.Productid = maxProductId;
            khohang.Quantity = infoProduct.Quantity;

            _context.Khohangs.Add(khohang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInfoProduct", new { id = infoProduct.ProductId }, infoProduct);
        }

        // DELETE: api/InfoProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfoProduct(int id)
        {
            if (_context.Products == null || _context.Khohangs==null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            var khohang=await _context.Khohangs.FindAsync(id);
            if (product == null || khohang ==null)
            {
                return NotFound();
            }

                _context.Products.Remove(product);
                _context.Khohangs.Remove(khohang);
                await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InfoProductExists(int id)
        {
            return (_context.InfoProduct?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
