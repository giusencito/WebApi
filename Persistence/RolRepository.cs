using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Model.Entity;
using WebApi.Domain.Model.Enum;
using WebApi.Domain.Repository;
using WebApi.Shared.Persistence;

namespace WebApi.Persistence
{
    public class RolRepository : BaseRepository, IRolRepository
    {
        public  RolRepository(AppDbContext context): base (context) {
        
        
        }

        public async Task AddRol(Rol rol)
        {
            await _context.Rols.AddAsync(rol);
        }

        public bool existsByName(string Name)
        {
            return _context.Rols.Any(a => a.Rolname == Enum.Parse<Rolname>(Name));
        }

        public async Task<Rol?> GetRolByName(string Name)
        {
            return await _context.Rols.FirstOrDefaultAsync(r => r.Rolname == Enum.Parse<Rolname>(Name));
        }

        public  async Task<IEnumerable<Rol>> GetRols()
        {
            return await _context.Rols.ToListAsync();

        }
    }
}
