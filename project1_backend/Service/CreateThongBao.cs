using Microsoft.EntityFrameworkCore;
using project1_backend.Models;

namespace project1_backend.Service
{
    
    public class CreateThongBao
    {
        private readonly ProjectBongDaContext _context;

        public CreateThongBao(ProjectBongDaContext context)
        {
            _context = context;
        }
        public void create(Thongbao thongbao)
        {
            if (_context.Thongbaos == null)
            {
                return;
            }
            _context.Thongbaos.Add(thongbao);
            _context.SaveChangesAsync();

        }
    }
}
