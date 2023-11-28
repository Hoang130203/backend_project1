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
    public class ThongbaosController : ControllerBase
    {
        private readonly ProjectBongDaContext _context;

        public ThongbaosController(ProjectBongDaContext context)
        {
            _context = context;
        }

        // GET: api/Thongbaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Thongbao>>> GetThongbaos()
        {
          if (_context.Thongbaos == null)
          {
              return NotFound();
          }
            return await _context.Thongbaos.ToListAsync();
        }

        // GET: api/Thongbaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Thongbao>> GetThongbao(int id)
        {
          if (_context.Thongbaos == null)
          {
              return NotFound();
          }
            var thongbao = await _context.Thongbaos.FindAsync(id);

            if (thongbao == null)
            {
                return NotFound();
            }

            return thongbao;
        }

        // POST: api/Thongbaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Thongbao>> PostThongbao(Thongbao thongbao)
        {
          if (_context.Thongbaos == null)
          {
              return Problem("Entity set 'ProjectBongDaContext.Thongbaos'  is null.");
          }
            _context.Thongbaos.Add(thongbao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetThongbao", new { id = thongbao.Notifid }, thongbao);
        }

        // DELETE: api/Thongbaos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThongbao(int id)
        {
            if (_context.Thongbaos == null)
            {
                return NotFound();
            }
            var thongbao = await _context.Thongbaos.FindAsync(id);
            if (thongbao == null)
            {
                return NotFound();
            }

            _context.Thongbaos.Remove(thongbao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ThongbaoExists(int id)
        {
            return (_context.Thongbaos?.Any(e => e.Notifid == id)).GetValueOrDefault();
        }
    }
}
