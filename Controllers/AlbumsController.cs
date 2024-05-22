using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Model.Entity;
using WebApi.Domain.Service;
using WebApi.Resource;
using WebApi.Shared.Persistence;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly IMapper _mapper;

        public AlbumsController(IAlbumService albumService, IMapper mapper)
        {
            _mapper = mapper;
            _albumService = albumService;

        }



        // GET: api/Albums
        [HttpGet]
        public async Task<IEnumerable<AlbumResource>> GetAlbums()
        {
         
            var albums = await _albumService.GetAlbums();
            var resources = _mapper.Map<IEnumerable<Album>,IEnumerable<AlbumResource>>(albums);
            return resources;


        }

        // GET: api/Albums/5
        [HttpGet("{id}")]
        public async Task<AlbumResource> GetAlbum(long id)
        {
          var album = await _albumService.GetAlbumById(id);
          var resource = _mapper.Map<Album,AlbumResource>(album);
           return resource;
        }

        // PUT: api/Albums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(long id,[FromBody] SaveAlbumResource album)
        {
            var albumResource = _mapper.Map<SaveAlbumResource, Album>(album);

            var result = await _albumService.UpdateAlbum(id, albumResource);
            if(!result.Success)
            {
                return BadRequest(result.Message);
            }
            var resource = _mapper.Map<Album, SaveAlbumResource>(result.Resource);
            return Ok(resource);
        }

        // POST: api/Albums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostAlbum([FromBody] SaveAlbumResource album)
        {
            var albumResource = _mapper.Map<SaveAlbumResource, Album>(album);
            var result = await _albumService.AddAlbum(albumResource);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var resource = _mapper.Map<Album, SaveAlbumResource>(result.Resource);
            return Ok(resource);



        }

        // DELETE: api/Albums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(long id)
        {
            var result = await _albumService.DeleteAlbum(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var resource = _mapper.Map<Album,AlbumResource>(result.Resource);
            return Ok(resource);
        }

       
    }
}
