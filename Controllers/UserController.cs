using Microsoft.AspNetCore.Mvc;
using RouletteBetsApi.Models.Dtos;
using RouletteBetsApi.Models;
using RouletteBetsApi.Repositories;
using RouletteBetsApi.Services;
using RouletteBetsApi.Services.Interfaces;
using System.Runtime.CompilerServices;

namespace RouletteBetsApi.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;

        public UserController (UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signUp")]
        public async Task<ActionResult<User>> Create(User user)
        {
            return await _userService.Create(user);
        }
    }
}
