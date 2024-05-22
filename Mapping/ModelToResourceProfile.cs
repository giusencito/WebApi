using AutoMapper;
using WebApi.Domain.Model.Entity;
using WebApi.Resource;


namespace WebApi.Mapping
{
    public class ModelToResourceProfile : AutoMapper.Profile
    {
        public ModelToResourceProfile() {

            CreateMap<Album, AlbumResource>();
            CreateMap<Album,SaveAlbumResource>();
            CreateMap<Song, SaveSongResource>();
            CreateMap<Song,MusicResource>();
            CreateMap<User, UserResource>();

        }
    }
}
