using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RouletteBetsApi.Exceptions;
using RouletteBetsApi.Models;
using RouletteBetsApi.Models.Dtos;
using RouletteBetsApi.Services;

namespace RouletteBetsApi.Controllers
{
    [ApiController]
    [Route("v1/roulettes")]
    public class RouletteController : ControllerBase
    {
        private readonly RouletteService _rouletteService;
        public readonly IMapper _mapper;
        private IDistributedCache _cache;
        public RouletteController(RouletteService rouletteService, IMapper mapper,IDistributedCache cache) 
        { 
            _rouletteService = rouletteService;
            _mapper = mapper;
            _cache = cache;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Roulette>> GetRouletteById(string id)
        {
            string recordKey = "Roulette_" + DateTime.UtcNow.ToString();
            var roulette = await _cache.GetValueAsync<Roulette>(recordKey);
            if(roulette is null )
            {
                roulette = await _rouletteService.GetRouletteById(id);
                await _cache.SetValueAsync(recordKey, roulette);
            }
            return Ok(roulette);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> Create(RouletteDto rouletteDto)
        {
            if (rouletteDto == null)
                throw new BadRequestException("The Roulette object is null");
            else if (!(rouletteDto.state.ToUpper().Equals("OPEN") || rouletteDto.state.ToUpper().Equals("CLOSE")))
                throw new BadRequestException("The state field has to be 'OPEN' or 'CLOSE'");
            Roulette roulette = _mapper.Map<Roulette>(rouletteDto);
            string rouletteIdCreated = await _rouletteService.Create(roulette);
            return Ok("Roulette: " + rouletteIdCreated + "created");
        }
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Open(string id)
        {
                await _rouletteService.UpdateState(id, "OPEN");
                return Ok("Roulette: "+id+ " OPEN");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roulette>>> GetAll()
        {
            string recordKey = "Roulettes_" + DateTime.UtcNow.ToString();
            var roulettes = await _cache.GetValueAsync<List<Roulette>>(recordKey);
            if(roulettes == null)
            {
                roulettes = await _rouletteService.GetAll();
                await _cache.SetValueAsync(recordKey, roulettes);
            }
            return Ok(roulettes);
        }

    }
}
