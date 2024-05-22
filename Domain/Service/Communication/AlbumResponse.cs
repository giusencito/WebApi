using WebApi.Domain.Model.Entity;
using WebApi.Shared.Persistence;

namespace WebApi.Domain.Service.Communication
{
    public class AlbumResponse : BaseResponse<Album>
    {
        public AlbumResponse(string message) : base(message)
        {
        }

        public AlbumResponse(Album resource) : base(resource)
        {
        }
    }
}
