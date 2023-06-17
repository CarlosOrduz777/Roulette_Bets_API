using Microsoft.AspNetCore.Mvc;
using RouletteBetsApi.Models;
using RouletteBetsApi.Repositories;

namespace RouletteBetsApi.Controllers
{
    [ApiController]
    [Route("api/v1/roulettes")]
    public class RouletteController : ControllerBase
    {
        private readonly RouletteService _rouletteService;
        public RouletteController(RouletteService rouletteService) 
        { 
            _rouletteService = rouletteService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Roulette>> GetRouletteById(string id)
        {
            return await _rouletteService.GetRouletteById(id);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(Roulette roulette)
        {
            return await _rouletteService.Create(roulette);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Open(string id)
        {
            try
            {
                await _rouletteService.UpdateState(id, "OPEN");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roulette>>> GetAll()
        {
            return await _rouletteService.GetAll();
        }

    }
}
