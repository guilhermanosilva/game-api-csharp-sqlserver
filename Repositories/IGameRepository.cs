using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using game.api.Entities;

namespace game.api.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> GetGame(int page, int amount);
        Task<List<Game>> GetGame(string name, string producer);
        Task<Game> GetGame(Guid id);
        Task InsertGame(Game game);
        Task UpdateGame(Game game);
        Task DeleteGame(Guid id);
    }
}