using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouletteBetsApi.Exceptions;
using RouletteBetsApi.Models;
using RouletteBetsApi.Models.Dtos;
using RouletteBetsApi.Repositories;
using RouletteBetsApi.Services;
using RouletteBetsApi.Services.Interfaces;
using System.Collections;

namespace RouletteBetsApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/v1/bets")]
    public class BetController: ControllerBase
    {
        private GameService gameService;
        private readonly IBetService _betService;
        private readonly IRouletteService _rouletteService;
        public readonly IMapper _mapper;
        
        public BetController(BetService betService, RouletteService rouletteService, IMapper mapper)
        {
            _rouletteService = rouletteService;
            _betService = betService;
            _mapper = mapper;
            gameService = new GameService();
        }
        /// <summary>
        /// Creates a Bet
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Bet>> Create(BetDto betDto)
        {
            Console.WriteLine(betDto.color.ToLower().Equals("red"));
            if (!(betDto.color.ToLower().Equals("red") || betDto.color.ToLower().Equals("black")))
                throw new BadRequestException("'color' field has to be 'red' or 'black'");
            Bet bet = _mapper.Map<Bet>(betDto);
            if (!isAuthorized())
                throw new NotauthorizedAccessException("You don't have authorization please log in");
            if (!ModelState.IsValid || !gameService.IsValid(betDto) || !await gameService.IsRouletteAvailable(bet, this._rouletteService))
                throw new BadHttpRequestException("Model Object invalid");
            bet.state = "PLAYING";
            bet.userId = Request.Headers["Authorization"];
            return await _betService.Create(bet);
        }
        [HttpPut]
        public async Task<ActionResult<IEnumerable<Bet>>> Close([FromQuery]string rouletteId)
        {
            var bets = await _betService.GetByRouletteId(rouletteId);
            if (bets == null)
                throw new BadRequestException("Roulette id not found");
            await _rouletteService.UpdateState(rouletteId, "CLOSED");
            bets = gameService.CalculateWinners(bets);
            List<Bet> betListUpdated = new(bets); ;
            bets.ForEach(async x => {
                _betService.Update(x);
            });
            bets.ForEach(async b =>
            {
                await _betService.Delete(b._id);
            });
            return betListUpdated;
        }

        private bool isAuthorized()
        {
            string headerValue = string.Empty;
            if (Request.Headers.ContainsKey("Authorization"))
                headerValue = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(headerValue))
                return false;
            else
                return true;
        }
    }
}
