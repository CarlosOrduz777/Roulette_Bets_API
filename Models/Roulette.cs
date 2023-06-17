using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RouletteBetsApi.Models
{
    public class Roulette
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id {  get; set; }
        public string name { get; set; }
        public string state { get; set; } 
    }
}
