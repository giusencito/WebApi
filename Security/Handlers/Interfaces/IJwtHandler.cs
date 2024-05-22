using WebApi.Domain.Model.Entity;

namespace WebApi.Security.Handlers.Interfaces
{
    public interface IJwtHandler
    {
        public string GenerateToken(User user);
        public Boolean ValidateToken(string token);

        public string GetUserNameFromToken(string token);
        public string[] GetRolesFromToken(string token);
    }
}
