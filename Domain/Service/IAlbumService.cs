using WebApi.Domain.Model.Entity;
using WebApi.Domain.Service.Communication;

namespace WebApi.Domain.Service
{
    public interface IAlbumService
    {
        Task<IEnumerable<Album>> GetAlbums();
        Task<AlbumResponse> AddAlbum(Album album);

        Task<AlbumResponse> UpdateAlbum(long id, Album album);
        Task<AlbumResponse> DeleteAlbum(long id);

        Task<Album> GetAlbumById(long id);

        Task<IEnumerable<Album>> GetAlbumsByName(String name);
    }
}
