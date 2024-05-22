using System.Xml.Linq;
using WebApi.Domain.Model.Entity;
using WebApi.Domain.Repository;
using WebApi.Domain.Service;
using WebApi.Domain.Service.Communication;
using WebApi.Persistence;
using WebApi.Shared.Persistence;

namespace WebApi.Service
{
    public class SongService : ISongService
    {

        private readonly ICategoryRepository _songRepository;
        private readonly IAlbumRepository _albumRepository;

        private readonly IUnitOfWork _unitOfWork;

        public SongService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IAlbumRepository albumRepository)
        {
            _songRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _albumRepository = albumRepository;
        }



        public async Task<SongResponse> AddSong(long id,Song song)
        {
            var album = await _albumRepository.GetAlbumById(id);
            if (album == null)
            {
                return new SongResponse("Not Found");
            }
            try
            {
                song.AlbumId = id;
                await _songRepository.AddSong(song);
                await _unitOfWork.CompleteAsync();
                return new SongResponse(song);
            }
            catch (Exception ex)
            {
                return new SongResponse($"Error saving: {ex.Message}");

            }

        }

        public async Task<SongResponse> DeleteSong(long id)
        {
            var song = await _songRepository.GetSongById(id);
            if (song  == null)
            {
                return new SongResponse("Not found");
            }
            try
            {
                _songRepository.DeleteSong(song);
                await _unitOfWork.CompleteAsync();
                return new SongResponse(song);
            }
            catch (Exception ex)
            {
                return new SongResponse($"Error deleting: {ex.Message}");
            }
        }

        public async Task<Song> GetSongById(long id)
        {
            var song = await _songRepository.GetSongById(id);
            await _unitOfWork.CompleteAsync();
            if (song == null) throw new KeyNotFoundException("Not Found");
            return song;
        }

        public async Task<IEnumerable<Song>> GetSongs()
        {
            return await _songRepository.GetSongs();
        }

        public async Task<IEnumerable<Song>> GetSongsByAlbum(long id)
        {
            return await _songRepository.GetSongsByAlbum(id);

        }

        public async Task<SongResponse> UpdateSong(long id, Song song)
        {
            var existedSong = await _songRepository.GetSongById(id);
            if (existedSong == null)
            {
                return new SongResponse("Not found");
            }
            existedSong.Name = song.Name;
            existedSong.MusicUrl = song.MusicUrl;
            try
            {
                _songRepository.UpdateSong(existedSong);
                await _unitOfWork.CompleteAsync();
                return new SongResponse(existedSong);
            }
            catch (Exception ex)
            {
                return new SongResponse($"Error deleting: {ex.Message}");

            }
        }
    }
}
