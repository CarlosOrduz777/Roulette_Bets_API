using Microsoft.AspNetCore.Mvc;
using RouletteBetsApi.Models.Dtos;
using RouletteBetsApi.Models;
using RouletteBetsApi.Repositories;
using RouletteBetsApi.Services;
using RouletteBetsApi.Services.Interfaces;
using System.Runtime.CompilerServices;
using AutoMapper;

namespace RouletteBetsApi.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;
        public readonly IMapper _mapper;

        public UserController (UserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("signUp")]
        public async Task<ActionResult<User>> Create(UserDto userdto)
        {   
            var user = _mapper.Map<User>(userdto);
            return await _userService.Create(user);
        }
    }
}
