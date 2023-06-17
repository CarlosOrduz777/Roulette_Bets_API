using System.ComponentModel.DataAnnotations;

namespace RouletteBetsApi.Models.Dtos
{
    public class RouletteDto
    {
        [Required(ErrorMessage = "'name' field is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "'state' field is required")]
        public string state { get; set; }
    }
}
