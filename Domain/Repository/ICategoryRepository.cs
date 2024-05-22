using WebApi.Domain.Model.Entity;

namespace WebApi.Domain.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Song>> GetSongs();
        Task AddSong(Song song);

        void UpdateSong(Song song);
        void DeleteSong(Song song);

        Task<Song?> GetSongById(long id);

        Task<IEnumerable<Song>> GetSongsByAlbum(long id);



    }
}
