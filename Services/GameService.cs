using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game.api.Entities;
using game.api.Models.InputModel;
using game.api.Models.ViewModel;
using game.api.Repositories;

namespace game.api.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> GetGame(int page, int amount)
        {
            var games = await _gameRepository.GetGame(page, amount);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Price = game.Price,
                Producer = game.Producer
            }).ToList();
        }

        public async Task<GameViewModel> GetGame(Guid id)
        {
            var game = await _gameRepository.GetGame(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Price = game.Price,
                Producer = game.Producer
            };
        }

        public async Task<GameViewModel> InsertGame(GameInputModel inputGame)
        {
            var game = await _gameRepository.GetGame(inputGame.Name, inputGame.Producer);

            if (game.Count() > 0)
                return null;

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = inputGame.Name,
                Price = inputGame.Price,
                Producer = inputGame.Producer
            };

            await _gameRepository.InsertGame(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = gameInsert.Name,
                Price = gameInsert.Price,
                Producer = gameInsert.Producer
            };
        }
  
        public async Task UpdateGame(Guid id, GameInputModel inputGame)
        {
            var game = await _gameRepository.GetGame(id);

            if(game == null)
                throw new Exception();
            
            game.Name = inputGame.Name;
            game.Price = inputGame.Price;
            game.Producer = inputGame.Producer;

            await _gameRepository.UpdateGame(game);
        }
 
        public async Task DeleteGame(Guid id)
        {
            var game = await _gameRepository.GetGame(id);

            if (game == null)
                throw new Exception();

            await _gameRepository.DeleteGame(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}