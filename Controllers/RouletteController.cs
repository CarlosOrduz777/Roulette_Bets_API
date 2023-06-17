using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RouletteBetsApi.Exceptions;
using RouletteBetsApi.Models;
using RouletteBetsApi.Models.Dtos;
using RouletteBetsApi.Repositories;

namespace RouletteBetsApi.Controllers
{
    [ApiController]
    [Route("api/v1/roulettes")]
    public class RouletteController : ControllerBase
    {
        private readonly RouletteService _rouletteService;
        public readonly IMapper _mapper;
        public RouletteController(RouletteService rouletteService, IMapper mapper) 
        { 
            _rouletteService = rouletteService;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Roulette>> GetRouletteById(string id)
        {
            return await _rouletteService.GetRouletteById(id);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(RouletteDto rouletteDto)
        {
            if (rouletteDto == null)
                throw new BadRequestException("The Roulette object is null");
            else if (!(rouletteDto.state.ToUpper().Equals("OPEN") || rouletteDto.state.ToUpper().Equals("CLOSE")))
                throw new BadRequestException("The state field has to be 'OPEN' or 'CLOSE'");
            Roulette roulette = _mapper.Map<Roulette>(rouletteDto);
            return await _rouletteService.Create(roulette);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Open(string id)
        {
                await _rouletteService.UpdateState(id, "OPEN");
                return NoContent();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roulette>>> GetAll()
        {
            return await _rouletteService.GetAll();
        }

    }
}
