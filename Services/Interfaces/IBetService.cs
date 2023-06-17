﻿using RouletteBetsApi.Models;
using RouletteBetsApi.Models.Dtos;

namespace RouletteBetsApi.Services.Interfaces
{
    public interface IBetService
    {
        Task<Bet> Create(Bet bet);
        Task<List<Bet>> GetByRouletteId(string rouletteId);
        void Update(Bet bet);
        Task Delete(string betId);
    }
}