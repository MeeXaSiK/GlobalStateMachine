using UnityEngine;

namespace NTC.GlobalStateMachine
{
    public abstract class StateMachineUser : MonoBehaviour
    {
        private void Awake()
        {
            BindCallbacks();
            
            OnAwake();
        }

        private void OnDestroy()
        {
            this.RemoveSubscriber();
            
            OnDestroyOverridable();
        }

        private void BindCallbacks()
        {
            this.On<RunningState>(OnGameRun);
            this.On<WinState>(OnGameWin);
            this.On<LoseState>(OnGameLose);
            this.On<WinState, LoseState>(OnGameFinish);
        }

        protected virtual void OnAwake() { }
        protected virtual void OnDestroyOverridable() { }
        protected virtual void OnGameRun() { }
        protected virtual void OnGameWin() { }
        protected virtual void OnGameLose() { }
        protected virtual void OnGameFinish() { }
    }
}