﻿using RouletteBetsApi.Models;

namespace RouletteBetsApi.Repositories
{
    public interface IBetService
    {
        Task<Bet> Create(Bet bet);
        Task<List<Bet>> GetByRouletteId(string rouletteId);
        void Update(Bet bet);
    }
}
