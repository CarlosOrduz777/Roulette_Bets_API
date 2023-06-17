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
        [Required(ErrorMessage ="'name' field is required")]
        public string name { get; set; }
        [Required(ErrorMessage ="'state' field is required")]
        public string state { get; set; } 
    }
}
