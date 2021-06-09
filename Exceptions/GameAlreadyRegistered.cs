using System;

namespace game.api.Exceptions
{
    public class GameAlreadyRegistered : Exception
    {
        public GameAlreadyRegistered() : base("This game is already registered") { }
    }
}