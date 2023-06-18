using RouletteBetsApi.Models;

namespace RouletteBetsApi.Repositories.Interfaces
{
    public interface IRouletteRepository
    {
        Task<string> Create(Roulette roulette);
        Task UpdateState(string rouletteId, string state);
        Task<Roulette> GetRouletteById(string rouletteId);
        Task<List<Roulette>> GetAll();
    }
}
