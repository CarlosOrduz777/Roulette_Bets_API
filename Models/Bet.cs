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
        [Range(0,36, ErrorMessage ="The valid numbers to bet are from 0 to 36")]
        public int number { get; set; }
        public string color { get; set; }
        [Required(ErrorMessage = "'value' field is required")]
        [Range(0.001, 10000.0,ErrorMessage ="Maximum US10.000 to bet")]
        public decimal quantity { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [Required(ErrorMessage ="'rouletteId' field is required")]
        public string rouletteId { get; set; }
        public decimal earnedQuantity { get; set; }
        public string userId { get; set; }
        public string state { get; set; }
    }
}
