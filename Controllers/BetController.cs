using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RouletteBetsApi.Exceptions;
using RouletteBetsApi.Models;
using RouletteBetsApi.Models.Dtos;
using RouletteBetsApi.Repositories;
using RouletteBetsApi.Repositories.Interfaces;
using RouletteBetsApi.Services;
using System.Collections;
using System.Net.Http.Headers;

namespace RouletteBetsApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/bets")]
    public class BetController: ControllerBase
    {
        private GameService gameService;
        private readonly BetService _betService;
        private readonly RouletteService _rouletteService;
        public readonly IMapper _mapper;
        private IDistributedCache _cache;


        public BetController(BetService betService, RouletteService rouletteService, IMapper mapper,IDistributedCache cache)
        {
            _rouletteService = rouletteService;
            _betService = betService;
            _mapper = mapper;
            gameService = new GameService();
            _cache = cache;
        }
        [HttpPost]
        public async Task<ActionResult<Bet>> Create(BetDto betDto)
        {
            if (!(betDto.color.ToLower().Equals("red") || betDto.color.ToLower().Equals("black")))
                throw new BadRequestException("'color' field has to be 'red' or 'black'");
            Bet bet = _mapper.Map<Bet>(betDto);
            if (!isAuthorized())
                throw new NotauthorizedAccessException("You don't have authorization please log in");
            if (!ModelState.IsValid || !gameService.IsBetValid(betDto) || !await gameService.IsRouletteAvailable(bet, this._rouletteService))
                throw new BadHttpRequestException("Model Object invalid");
            bet.state = "PLAYING";
            var userId = User.Claims.FirstOrDefault(x => x.Type == "_id")?.Value;
            bet.userId = userId;
            Bet createdBet = await _betService.Create(bet);
            return createdBet;
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
