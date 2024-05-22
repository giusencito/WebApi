using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Model.Entity;
using WebApi.Domain.Service;
using WebApi.Domain.Service.Communication;
using WebApi.Resource;
using WebApi.Security.Attributes;
using WebApi.Service;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRolService _rolService;

        public UserController(IUserService userService, IMapper mapper, IRolService rolService)

        {
            _userService = userService;
            _mapper = mapper;
            _rolService = rolService;

        }
        [AllowAnonymous]
        [HttpPost("auth/sign-in")]
        public async Task<IActionResult> Authenticate(Login request)
        {
            var response = await _userService.Login(request);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("auth/sign-up")]
        public async Task<IActionResult> Register(Register request)
        {
            await _userService.Register(request);
            return Ok(new { message = "Registration successful." });
        }
        [HttpGet]
        public async Task<IEnumerable<UserResource>> GetUserss()
        {

            var users = await _userService.GetUsers();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return resources;


        }



    }
}
