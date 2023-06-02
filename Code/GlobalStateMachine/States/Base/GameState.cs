using System.Collections.Generic;
using UnityEngine;

namespace NTC.GlobalStateMachine
{
    public abstract class GameState
    {
        public virtual bool CanRepeat => true;

        private readonly List<int> _blockedNextStateIndexes = new List<int>(8);
        private bool _isBlockedStatesSetup;

        public virtual bool IsNextStatePossible<TState>() where TState : GameState
        {
            SetupBlockingNextStates();
            
            var nextStateIndex = StateIndex<TState>.Index;

            for (var i = 0; i < _blockedNextStateIndexes.Count; i++)
            {
                if (_blockedNextStateIndexes[i] == nextStateIndex)
                {
                    return false;
                }
            }

            return true;
        }
        
        public bool Is<T>() where T : GameState
        {
            return GetType() == typeof(T);
        }

        protected void BlockNextState<TState>() where TState : GameState
        {
            var nextStateIndex = StateIndex<TState>.Index;

            for (var i = 0; i < _blockedNextStateIndexes.Count; i++)
            {
                if (_blockedNextStateIndexes[i] == nextStateIndex)
                {
#if DEBUG
                    Debug.LogError($"You are trying to block {typeof(TState).Name} twice in the {GetType().Name}");
#endif
                    return;
                }
            }
            
            _blockedNextStateIndexes.Add(nextStateIndex);
        }

        private void SetupBlockingNextStates()
        {
            if (_isBlockedStatesSetup)
                return;

            BlockNextStates();
            
            _isBlockedStatesSetup = true;
        }

        protected virtual void BlockNextStates() { }
    }
}