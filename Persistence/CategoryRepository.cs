using WebApi.Shared.Persistence;
using WebApi.Domain.Repository;
using WebApi.Domain.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace WebApi.Persistence
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {


        }
        public async Task AddSong(Song song)
        {
            await _context.Songs.AddAsync(song);
        }

        public void DeleteSong(Song song)
        {
             _context.Songs.Remove(song);
         }

        public async Task<Song?> GetSongById(long id)
        {
            return await _context.Songs.Include(s => s.Album).FirstOrDefaultAsync(s => s.Id == id);

        }

        public async Task<IEnumerable<Song>> GetSongs()
        {
            return await _context.Songs.Include(s => s.Album).ToListAsync();

        }

        public async Task<IEnumerable<Song>> GetSongsByAlbum(long id)
        {
             return await _context.Songs.Where(a => a.AlbumId == id).Include(s => s.Album).ToListAsync();

        }

        public void UpdateSong(Song song)
        {
            _context.Songs.Update(song);
        }
    }
}
