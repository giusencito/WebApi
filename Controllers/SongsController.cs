using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using Org.BouncyCastle.Utilities;
using WebApi.Domain.Model.Entity;
using WebApi.Domain.Service;
using WebApi.Resource;
using WebApi.Service;
using WebApi.Shared.Persistence;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongService _songService ;
        private readonly IMapper _mapper;

        public SongsController  (ISongService songService, IMapper mapper)
        {
            _songService = songService;
            _mapper = mapper;
        }



        // GET: api/Songs
        [HttpGet]
        public async Task<IEnumerable<MusicResource>> GetSongs()
        {
            var songs = await _songService.GetSongs();
            var resources = _mapper.Map<IEnumerable<Song>, IEnumerable<MusicResource>>(songs);
            return resources;
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<MusicResource> GetSong(long id)
        {
            var song = await _songService.GetSongById(id);

            var resource = _mapper.Map<Song, MusicResource>(song);
            return resource;
        }
        [HttpGet("albums/{id}")]
        public async Task<IEnumerable<MusicResource>> GetSongsByAlbum(long id)
        {
            var songs = await _songService.GetSongsByAlbum(id);
            var resources = _mapper.Map<IEnumerable<Song>, IEnumerable<MusicResource>>(songs);
            return resources;
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(long id,[FromBody] SaveSongResource song)
        {
            var songResource = _mapper.Map<SaveSongResource, Song>(song);

            var result = await _songService.UpdateSong(id, songResource);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var resource = _mapper.Map<Song, SaveSongResource>(result.Resource);
            return Ok(resource);
        }

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostSong(long id ,[FromBody] SaveSongResource song)
        {
            var songResource = _mapper.Map<SaveSongResource, Song>(song);
            var result = await _songService.AddSong(id,songResource);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var resource = _mapper.Map<Song, SaveSongResource>(result.Resource);
            return Ok(resource);

        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(long id)
        {
            var result = await _songService.DeleteSong(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var resource = _mapper.Map<Song, MusicResource>(result.Resource);
            return Ok(resource);
        }

       
    }
}
