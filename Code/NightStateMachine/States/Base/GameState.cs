using System;

namespace NTC.Global.StateMachine
{
    public abstract class GameState
    {
        private readonly Type _type;

        public GameState()
        {
            _type = GetType();
        }
        
        public bool Is<T>() where T : GameState
        {
            return _type == typeof(T);
        }
    }
}