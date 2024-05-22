using WebApi.Domain.Model.Entity;
using WebApi.Domain.Service.Communication;

namespace WebApi.Domain.Service
{
    public interface ISongService
    {

        Task<IEnumerable<Song>> GetSongs();
        Task<SongResponse> AddSong(long id,Song song);

        Task<SongResponse> UpdateSong(long id, Song song);
        Task<SongResponse> DeleteSong(long id);

        Task<Song> GetSongById(long id);

        Task<IEnumerable<Song>> GetSongsByAlbum(long id);


    }
}
