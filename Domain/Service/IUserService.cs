using WebApi.Domain.Model.Entity;
using WebApi.Domain.Service.Communication;

namespace WebApi.Domain.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<UserResponse> AddUser(User user);
        Task<UserResponse> UpdateUser(long id, User user);
        Task<UserResponse> DeleteUser(long id);
        Task<User> GetUserById(long id);
        Task<User> GetUserByUserName(string username);

        Task<jwtDto> Login(Login login); 

        Task Register(Register register);



    }
}
