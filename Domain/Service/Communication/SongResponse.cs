using WebApi.Domain.Model.Entity;
using WebApi.Shared.Persistence;

namespace WebApi.Domain.Service.Communication
{
    public class SongResponse : BaseResponse<Song>
    {
        public SongResponse(string message) : base(message)
        {
        }

        public SongResponse(Song resource) : base(resource)
        {
        }
    }
}
