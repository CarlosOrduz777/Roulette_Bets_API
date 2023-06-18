using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RouletteBetsApi.Configurations;
using RouletteBetsApi.Models;
using RouletteBetsApi.Repositories;
using RouletteBetsApi.Repositories.Interfaces;

namespace RouletteBetsApi.Services
{
    public class RouletteService
    {
        private readonly IRouletteRepository _roulettes;

        public RouletteService(RouletteRepository roulettes)
        {
            _roulettes = roulettes;
        }

        public async Task<string> Create(Roulette roulette)
        {
            return await _roulettes.Create(roulette);
        }

        public async Task<List<Roulette>> GetAll()
        {
            return await _roulettes.GetAll();
        }

        public async Task<Roulette> GetRouletteById(string rouletteId)
        {
            return await _roulettes.GetRouletteById(rouletteId);
        }

        public async Task UpdateState(string rouletteId, string state)
        {
           await _roulettes.UpdateState(rouletteId, state);
        }
    }
}
