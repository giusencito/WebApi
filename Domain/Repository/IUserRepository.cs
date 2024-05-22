using WebApi.Domain.Model.Entity;

namespace WebApi.Domain.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListUsers();
        Task AddUser(User user);
        Task<User?> FindUserById(long id);
        Task<User?> FindByUsername(string username);
        Task<User?> FindByUsernameForToken(string username);

        bool ExistsByUsername(string username);
        void Update(User user);
        void Remove(User user);
        Task<User?> FindByEmail(string email);

    }
}
