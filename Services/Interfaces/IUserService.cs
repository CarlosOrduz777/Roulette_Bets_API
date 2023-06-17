using RouletteBetsApi.Models;

namespace RouletteBetsApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> Create(User user);
        Task<User> GetUser(string email, string password);

    }
}
