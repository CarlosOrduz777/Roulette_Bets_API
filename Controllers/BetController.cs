using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RouletteBetsApi.Models;
using RouletteBetsApi.Models.Dtos;
using RouletteBetsApi.Repositories;
using RouletteBetsApi.Services;

namespace RouletteBetsApi.Controllers
{
    [ApiController]
    [Route("/api/v1")]
    public class BetController: ControllerBase
    {
        private GameService gameService;
        private readonly BetService _betService;
        private readonly RouletteService _rouletteService;
        public readonly IMapper _mapper;
        public BetController(BetService betService, RouletteService rouletteService, IMapper mapper)
        {
            _rouletteService = rouletteService;
            _betService = betService;
            _mapper = mapper;
            gameService = new GameService();
        }
        
        [HttpPost]
        public async Task<ActionResult<Bet>> Create(BetDto betDto)
        {
            try
            {
                if (!isAuthorized())
                    return Unauthorized("Yo have to be Authorized to do this action");
                if (!ModelState.IsValid || !gameService.IsValid(betDto) || !await gameService.IsRouletteAvailable(betDto, this._rouletteService))
                    return BadRequest("Invalid Model Object");

                betDto.state = "PLAYING";
                betDto.userId = Request.Headers["Authorization"];
                Bet bet = _mapper.Map<Bet>(betDto);
                return await _betService.Create(bet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut]
        public async Task<ActionResult<IEnumerable<Bet>>> Close([FromQuery] string rouletteId)
        {
            var bets = await _betService.GetByRouletteId(rouletteId);
            await _rouletteService.UpdateState(rouletteId, "CLOSED");
            gameService.calculateWinners(bets);
            bets.ForEach(x => _betService.Update(x));
            return bets;
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
