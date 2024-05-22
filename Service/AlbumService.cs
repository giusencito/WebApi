using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApi.Domain.Model.Entity;
using WebApi.Domain.Repository;
using WebApi.Domain.Service;
using WebApi.Shared.Persistence;
using WebApi.Domain.Service.Communication;

namespace WebApi.Service
{
    public class AlbumService : IAlbumService
    {
        private readonly  IAlbumRepository _albumRepository;
        private readonly  IUnitOfWork _unitOfWork;

        public AlbumService(IAlbumRepository albumRepository, IUnitOfWork unitOfWork) {
            _albumRepository = albumRepository;
            _unitOfWork = unitOfWork;   
        
        
        }
        public async Task<AlbumResponse> AddAlbum(Album album)
        {
            try
            {
                await _albumRepository.AddAlbum(album);
                await _unitOfWork.CompleteAsync();
                return new AlbumResponse(album); 
            }
            catch (Exception ex) {

                return new AlbumResponse($"Error saving: {ex.Message}");
            
            }
            

            
        }

        public async Task<AlbumResponse> DeleteAlbum(long id )
        {
            var album = await _albumRepository.GetAlbumById(id);
            if(album == null)
            {
                return new AlbumResponse("Not found");
            }
            try
            {
                _albumRepository.DeleteAlbum(album);
                await _unitOfWork.CompleteAsync();
                return new AlbumResponse(album);
            }
            catch (Exception ex)
            {
                return new AlbumResponse($"Error deleting: {ex.Message}");
            }
        }

        public async Task<Album> GetAlbumById(long id)
        {
            var album = await _albumRepository.GetAlbumById(id);
            await _unitOfWork.CompleteAsync();
            if(album == null) throw new KeyNotFoundException("Not Found");
            return album;
        }

        public async Task<IEnumerable<Album>> GetAlbums()
        {
            return await _albumRepository.GetAlbums();

        }

        public async Task<IEnumerable<Album>> GetAlbumsByName(string name)
        {
            return await _albumRepository.GetAlbumsByName(name);
        }

        public async Task<AlbumResponse> UpdateAlbum(long id, Album album)
        {
            var existedAlbum = await _albumRepository.GetAlbumById(id);
            if (existedAlbum == null)
            {
                return new AlbumResponse("Not found");
            }
            existedAlbum.Name = album.Name;
            existedAlbum.Description = album.Description;
            try
            {
                _albumRepository.UpdateAlbum(existedAlbum);
                await _unitOfWork.CompleteAsync();
                return new AlbumResponse(existedAlbum);
            }
            catch (Exception ex)
            {
                return new AlbumResponse($"Error deleting: {ex.Message}");

            }

        }
    }
}
