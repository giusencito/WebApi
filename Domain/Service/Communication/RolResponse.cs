using WebApi.Domain.Model.Entity;
using WebApi.Shared.Persistence;

namespace WebApi.Domain.Service.Communication
{
    public class RolResponse : BaseResponse<Rol>
    {
        public RolResponse(string message) : base(message)
        {
        }

        public RolResponse(Rol resource) : base(resource)
        {
        }
    }
}
