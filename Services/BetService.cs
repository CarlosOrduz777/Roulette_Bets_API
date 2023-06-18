using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RouletteBetsApi.Configurations;
using RouletteBetsApi.Models;
using RouletteBetsApi.Models.Dtos;
using RouletteBetsApi.Repositories.Interfaces;

namespace RouletteBetsApi.Repositories
{
    public class BetService
    {
        private readonly IBetRepository _bets;

        public BetService(BetRepository bets)
        {
            _bets = bets;
        }
        public async Task<Bet> Create(Bet bet)
        {
            return await _bets.Create(bet);
        }

        public async Task Delete(String betId)
        {
            await _bets.Delete(betId);
        }

        public async Task<List<Bet>> GetByRouletteId(string rouletteId)
        {
            return await _bets.GetByRouletteId(rouletteId);
            
        }

        public void Update(Bet bet)
        {
            _bets.Update(bet);
        }
    }
}
