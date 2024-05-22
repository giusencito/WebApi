

using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Model.Entity;

namespace WebApi.Domain.Repository
{
    public interface IAlbumRepository
    {
        Task<IEnumerable<Album>> GetAlbums();
        Task AddAlbum(Album album);

        void UpdateAlbum(Album album);
        void DeleteAlbum(Album album);

        Task<Album?> GetAlbumById(long id);

        Task<IEnumerable<Album>> GetAlbumsByName(String name);





    }



}
