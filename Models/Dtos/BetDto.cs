namespace RouletteBetsApi.Models.Dtos
{
    public class BetDto
    {
        public int number { get; set; }
        public string color { get; set; }

        public decimal quantity { get; set; }
        public string rouletteId { get; set; }
    }
}
