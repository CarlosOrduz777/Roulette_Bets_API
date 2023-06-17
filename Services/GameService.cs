using Amazon.Runtime.Internal;
using RouletteBetsApi.Models;
using RouletteBetsApi.Models.Dtos;
using RouletteBetsApi.Repositories;
using System.Drawing;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RouletteBetsApi.Services
{
    public class GameService
    {
        
        public void calculateWinners(List<Bet> bets)
        {
            int numberWinner = new Random().Next(1, 36);
            string colorWinner = CalculateColorWinner(numberWinner);
            foreach (Bet bet in bets)
            {
                if (bet.number == numberWinner)
                {
                    bet.state = "WINNER";
                    bet.earnedQuantity = bet.earnedQuantity * 5;
                }
                else if (string.Equals(colorWinner, bet.color))
                {
                    bet.state = "WINNER";
                    bet.earnedQuantity = bet.earnedQuantity * 1.8m;
                }
                else
                    bet.state = "PLAYED";
            }
        }
        private string CalculateColorWinner(int number)
        {
            if (number % 2 == 0)
                return "rojo";
            else
                return "negro";
        }
        public async Task<bool> IsRouletteAvailable(BetDto betDto, RouletteService rouletteService)
        {
            
            Roulette roulette = await rouletteService.GetRouletteById(betDto.rouletteId);
            return roulette.state.Equals("OPEN");
            
        }
        public bool IsValid(BetDto betDto)
        {
                return (IsColorValid(betDto.color) || betDto.number > 0);
        }
        public bool IsColorValid(string color)
        {
            switch (color)
            {
                case "rojo":
                case "negro":
                case "red":
                case "black":
                    return true;
                default:
                    return false;
            }
        }
    }
}
