using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using game.api.Models.InputModel;
using game.api.Models.ViewModel;

namespace game.api.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> GetGame(int page, int amount);
        Task<GameViewModel> GetGame(Guid id);
        Task<GameViewModel> InsertGame(GameInputModel game);
        Task UpdateGame(Guid id, GameInputModel game);
        Task DeleteGame(Guid id);

    }
}