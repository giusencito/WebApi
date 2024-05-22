using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Ocsp;
using WebApi.Domain.Model.Entity;
using WebApi.Domain.Model.Enum;
using WebApi.Domain.Repository;
using WebApi.Domain.Service;
using WebApi.Domain.Service.Communication;
using WebApi.Persistence;
using WebApi.Security.Handlers.Interfaces;
using WebApi.Shared;
using WebApi.Shared.Persistence;
using BCryptNet = BCrypt.Net.BCrypt;
namespace WebApi.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtHandler _jwtHandler;
        private readonly IRolRepository _rolRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository ,IUnitOfWork unitOfWork,IJwtHandler jwtHandler,IMapper mapper,IRolRepository rolRepository) { 
        
            _unitOfWork = unitOfWork;   
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
                     _rolRepository = rolRepository;
            _mapper = mapper;


        }
        public async Task<UserResponse> AddUser(User user)
        {
            try
            {
                await _userRepository.AddUser(user);
                await _unitOfWork.CompleteAsync();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {

                return new UserResponse($"Error saving: {ex.Message}");

            }
        }

        public async Task<UserResponse> DeleteUser(long id)
        {
            var user = await _userRepository.FindUserById(id);
            if (user == null)
            {
                return new UserResponse("Not found");
            }
            try
            {
                _userRepository.Remove(user);
                await _unitOfWork.CompleteAsync();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error deleting: {ex.Message}");
            }
        }

        public async Task<User> GetUserById(long id)
        {
            var user = await _userRepository.FindUserById(id);
            await _unitOfWork.CompleteAsync();
            if (user == null) throw new KeyNotFoundException("Not Found");
            return user;
        }

        public async Task<User> GetUserByUserName(string username)
        {
            var user = await _userRepository.FindByUsername(username);
            await _unitOfWork.CompleteAsync();
            if (user == null) throw new KeyNotFoundException("Not Found");
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.ListUsers();

        }

        public async Task<jwtDto> Login(Login login)
        {
            var user = await _userRepository.FindByUsernameForToken(login.Username);
            if (user == null || !BCryptNet.Verify(login.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            return new jwtDto(_jwtHandler.GenerateToken(user));
            
        }

        public async Task Register(Register register)
        {
            if(_userRepository.ExistsByUsername(register.Username))
            {
                throw new AppException($"Username {register.Username} is already taken.");
            }
            User user = new User();
            user.Username = register.Username;
            user.FirstName  = register.FirstName;
            user.LastName = register.LastName;
            user.Email = register.Email;
            user.PasswordHash = BCryptNet.HashPassword(register.PasswordHash);
            Rol value = await _rolRepository.GetRolByName(Rolname.Role_Admin.ToString());
            user.roles.Add(value);
            try
            {
                await _userRepository.AddUser(user);
                await _unitOfWork.CompleteAsync();
                
            }
            catch (Exception ex)
            {

                throw new AppException($"An error occurred while saving the user: {user}");

            }


        }

        public async Task<UserResponse> UpdateUser(long id, User user)
        {
            var existedUser = await _userRepository.FindUserById(id);
            if (existedUser == null)
            {
                return new UserResponse("Not found");
            }
            existedUser.FirstName = user.FirstName;
            existedUser.LastName = user.LastName;
            existedUser.Email = user.Email;
            try
            {
                _userRepository.Update(existedUser);
                await _unitOfWork.CompleteAsync();
                return new UserResponse(existedUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error deleting: {ex.Message}");

            }
        }
    }
}
