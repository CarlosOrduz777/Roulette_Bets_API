using Microsoft.AspNetCore.Mvc;
using RouletteBetsApi.Models.Dtos;
using RouletteBetsApi.Models;
using RouletteBetsApi.Repositories;
using RouletteBetsApi.Services;
using System.Runtime.CompilerServices;
using AutoMapper;
using RouletteBetsApi.Repositories.Interfaces;

namespace RouletteBetsApi.Controllers
{
    [ApiController]
    [Route("v1/users")]
    public class UserController:ControllerBase
    {
        private readonly UserService _userService;
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
