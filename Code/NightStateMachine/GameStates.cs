using System;

namespace NTC.Global.StateMachine
{
    public abstract class GameState
    {
        private Type Type => cachedType ??= GetType();
        private Type cachedType;
        
        public bool Is<T>() where T : GameState => 
            Type == typeof(T);
    }
    
    public sealed class RunningState : GameState { }
    public sealed class WinState : GameState { }
    public sealed class LoseState : GameState { }
}