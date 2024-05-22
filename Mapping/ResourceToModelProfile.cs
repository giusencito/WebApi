using WebApi.Domain.Model.Entity;
using WebApi.Resource;

namespace WebApi.Mapping
{
    public class ResourceToModelProfile : AutoMapper.Profile
    {




        public ResourceToModelProfile()
        {
            CreateMap<SaveSongResource, Song>();
            CreateMap<SaveAlbumResource, Album>();




        }
    }
}
