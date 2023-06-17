using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RouletteBetsApi.Configurations;
using RouletteBetsApi.Models;
using RouletteBetsApi.Services.Interfaces;

namespace RouletteBetsApi.Services
{
    public class UserService:IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IOptions<RouletteBetsDBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionURI);
            _users = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<User>(options.Value.UsersCollectionName);
        }

       

        public async Task<User> Create(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await _users.Find<User>(u => u.Email == email && u.Password == password).FirstAsync();
        }
    }
}
