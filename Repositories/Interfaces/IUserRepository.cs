using RouletteBetsApi.Models;

namespace RouletteBetsApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<User> GetUser(string email, string password);

    }
}
