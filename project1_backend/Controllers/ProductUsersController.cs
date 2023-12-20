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
        [HttpGet("FindComment")]
        public async Task<ActionResult<ProductUser>> GetProductUser(int productid,string phone,DateTime time)
        {
          if (_context.ProductUsers == null)
          {
              return NoContent();
          }
            var productUser = await _context.ProductUsers.FirstOrDefaultAsync(p=>p.Productid==productid 
            && p.Userphonenumber==phone &&p.Time.Date==time.Date && p.Time.Hour==time.Hour&&p.Time.Minute==time.Minute &&p.Time.Second==time.Second);

            if (productUser == null)
            {
                return NoContent();
            }

            return productUser;
        }
        //Get all comment
        [HttpGet("GetAllCmtOfProduct")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllComment(int productid)
        {
            if (_context.ProductUsers == null)
            {
                return NoContent();
            }
            var productUsers= await _context.ProductUsers.Where(p=>p.Productid==productid).OrderByDescending(p=>p.Time).ToListAsync();
            var listcomment=new List<object>();
            foreach(var p in productUsers)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u=>u.Phonenumber==p.Userphonenumber);
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
                if (!ProductUserExists(id,productUser.Userphonenumber,productUser.Time))
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
                var product_comment = await _context.ProductUsers.Where(pc => pc.Productid == productUser.Productid).ToListAsync();
                int rate = 0;
                foreach(var cmt in product_comment)
                {                   
                    rate += cmt.Rate??0;
                }
                rate = rate / product_comment.Count();
                var product= await _context.Products.FindAsync(productUser.Productid);
                if(product != null)
                {
                    product.Rate = rate;
                }
                await _context.SaveChangesAsync();
                var thongbao = new Thongbao();
                var user = await _context.Users.FindAsync(productUser.Userphonenumber);
                var productd =await _context.Products.FirstOrDefaultAsync(o=>o.Productid == productUser.Productid);

                thongbao.Orderid = productd.Productid;
                thongbao.Message = user.Name + " vừa đánh giá sản phẩm " + productd.Productname+ " \""+productUser.Comment+" \"";
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
                if (ProductUserExists(productUser.Productid,productUser.Userphonenumber,productUser.Time))
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

        private bool ProductUserExists(int id,string phone, DateTime time)
        {
            return (_context.ProductUsers?.Any(e => e.Productid == id && e.Userphonenumber==phone && e.Time.Minute==time.Minute)).GetValueOrDefault();
        }
    }
}
