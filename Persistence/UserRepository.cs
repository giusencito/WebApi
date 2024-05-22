using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Model.Entity;
using WebApi.Domain.Repository;
using WebApi.Shared.Persistence;

namespace WebApi.Persistence
{
    public class UserRepository : BaseRepository, IUserRepository
    {


        public UserRepository(AppDbContext context) : base(context)
        {


        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);    
        }

        public bool ExistsByUsername(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        public async Task<User?> FindByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync( u => u.Email == email);
        }

        public async Task<User?> FindByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        }

        public async Task<User?> FindByUsernameForToken(string username)
        {
            return await _context.Users
                       .Include(u => u.roles)                          
                       .FirstOrDefaultAsync(u => u.Username == username);


        }

        public async Task<User?> FindUserById(long id)
        {
            return await _context.Users.FindAsync(id);

        }

        public async Task<IEnumerable<User>> ListUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }




    }
}
