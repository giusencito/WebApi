using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using WebApi.Domain.Model.Entity;
using WebApi.Domain.Repository;
using WebApi.Shared.Persistence;

namespace WebApi.Persistence
{
    public class AlbumRepository : BaseRepository,IAlbumRepository

    {

        public AlbumRepository(AppDbContext context) : base(context) {

        
        }        
        public async Task AddAlbum(Album album)
        {
             await _context.Albums.AddAsync(album);
                
        }

        public void DeleteAlbum(Album album)
        {
            _context.Albums.Remove(album);
        }

        public async Task<Album?> GetAlbumById(long id)
        {
              return  await  _context.Albums.FindAsync(id);
        }

        public async Task<IEnumerable<Album>> GetAlbums()
        {
            return await _context.Albums.ToListAsync();
        }

        public async Task<IEnumerable<Album>> GetAlbumsByName(string name)
        {
            return await _context.Albums.Where(a => a.Name == name).ToListAsync();
        }

        public void UpdateAlbum(Album album)
        {
             _context.Albums.Update(album);
        }
    }
}
