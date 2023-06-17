using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RouletteBetsApi.Models;
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
        public IUnitOfWork _unitOfWork;
        // define the mapper
        public readonly IMapper _mapper;
        public BetController(BetService betService, RouletteService rouletteService)
        {
            _rouletteService = rouletteService;
            _betService = betService;
            gameService = new GameService();
        }
        [HttpPost]
        public async Task<ActionResult<Bet>> Create(Bet bet)
        {
            try
            {
                if (!isAuthorized())
                    return Unauthorized("Yo have to be Authorized to do this action");
                if (!ModelState.IsValid || !gameService.IsValid(bet) || !await gameService.IsRouletteAvailable(bet, this._rouletteService))
                    return BadRequest("Invalid Model Object");

                bet.state = "PLAYING";
                bet.userId = Request.Headers["Authorization"];

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
