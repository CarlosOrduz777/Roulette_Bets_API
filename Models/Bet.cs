using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RouletteBetsApi.Models
{
    public class Bet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public int number { get; set; }
        public string color { get; set; }
        public decimal quantity { get; set; }
        public string rouletteId { get; set; }
        public decimal earnedQuantity { get; set; }
        public string userId { get; set; }
        public string state { get; set; }
    }
}
