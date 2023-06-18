using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RouletteBetsApi.Configurations;
using RouletteBetsApi.Models;
using RouletteBetsApi.Repositories.Interfaces;

namespace RouletteBetsApi.Repositories
{
    public class BetRepository : IBetRepository

    {
        private readonly IMongoCollection<Bet> _bets;
        public BetRepository(IOptions<RouletteBetsDBSettings> options) {
            var mongoClient = new MongoClient(options.Value.ConnectionURI);
            _bets = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<Bet>(options.Value.BetsCollectionName);
        }
        public async Task<Bet> Create(Bet bet)
        {
            await _bets.InsertOneAsync(bet);
            return bet;
        }

        public async Task Delete(string betId)
        {
            FilterDefinition<Bet> filter = Builders<Bet>.Filter.Eq("_id", betId);
            await _bets.DeleteOneAsync(filter);
        }

        public async Task<List<Bet>> GetByRouletteId(string rouletteId)
        {
            return await _bets.Find<Bet>(b => b.rouletteId == rouletteId).ToListAsync(); ;
        }

        public async void Update(Bet bet)
        {
            await _bets.ReplaceOneAsync(b => b._id == bet._id, bet);
        }
    }
}
