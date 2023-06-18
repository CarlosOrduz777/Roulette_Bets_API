using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RouletteBetsApi.Configurations;
using RouletteBetsApi.Models;
using RouletteBetsApi.Repositories.Interfaces;

namespace RouletteBetsApi.Repositories
{
    public class RouletteRepository : IRouletteRepository
    {
        private readonly IMongoCollection<Roulette> _roulettes;
        public RouletteRepository(IOptions<RouletteBetsDBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionURI);
            _roulettes = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<Roulette>(options.Value.RoulettesCollectionName);
        }
        public async Task<string> Create(Roulette roulette)
        {
            await _roulettes.InsertOneAsync(roulette);
            return roulette._id;
        }

        public async Task<List<Roulette>> GetAll()
        {
            return await _roulettes.Find(r => true).ToListAsync();
        }

        public async Task<Roulette> GetRouletteById(string rouletteId)
        {
            return await _roulettes.Find(r => r._id == rouletteId).FirstOrDefaultAsync();
        }

        public async Task UpdateState(string rouletteId, string state)
        {
            var idFilter = Builders<Roulette>.Filter.Eq("_id", rouletteId);
            var toUpdate = Builders<Roulette>.Update.Set("state", state);
            await _roulettes.UpdateOneAsync(idFilter, toUpdate);
        }
    }
}
