using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RouletteBetsApi.Configurations;
using RouletteBetsApi.Models;
using RouletteBetsApi.Repositories;
using RouletteBetsApi.Repositories.Interfaces;

namespace RouletteBetsApi.Services
{
    public class UserService
    {
        private readonly IUserRepository _users;

        public UserService(UserRepository users)
        {
            _users = users;
        }

        public async Task<User> Create(User user)
        {
            return await _users.Create(user);
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await _users.GetUser(email, password);
        }
    }
}
