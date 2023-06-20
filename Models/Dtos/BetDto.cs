using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RouletteBetsApi.Models.Dtos
{
    public class BetDto
    {
        [Range(0, 36, ErrorMessage = "The valid numbers to bet are from 0 to 36")]
        public int number { get; set; }
        public string color { get; set; }
        [Required(ErrorMessage = "'quantity' field is required")]
        public decimal quantity { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [Required(ErrorMessage = "'rouletteId' field is required")]
        public string rouletteId { get; set; }
       
    }
}
