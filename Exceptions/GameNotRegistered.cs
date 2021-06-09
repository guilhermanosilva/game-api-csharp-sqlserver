using System;

namespace game.api.Exceptions
{
    public class GameNotRegistered : Exception
    {
        public GameNotRegistered() : base("This game is not registered") { }
    }
}