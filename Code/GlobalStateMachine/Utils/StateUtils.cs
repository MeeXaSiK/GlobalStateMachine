using System;

namespace NTC.GlobalStateMachine
{
    public static class StateUtils
    {
        public static GameState GetState(this GameStates state)
        {
            return state switch
            {
                GameStates.None => null,
                GameStates.Running => new RunningState(),
                GameStates.Win => new WinState(),
                GameStates.Lose => new LoseState(),
                
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }
    }
}