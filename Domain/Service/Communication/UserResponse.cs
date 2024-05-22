using WebApi.Domain.Model.Entity;
using WebApi.Shared.Persistence;

namespace WebApi.Domain.Service.Communication
{
    public class UserResponse : BaseResponse<User>
    {
        public UserResponse(string message) : base(message)
        {
        }

        public UserResponse(User resource) : base(resource)
        {
        }
    }
}
