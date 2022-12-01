using UnityEngine;

namespace NTC.Global.StateMachine
{
    public abstract class StateMachineUser : MonoBehaviour
    {
        private void Awake()
        {
            BindCallbacks();
            
            OnAwake();
        }

        private void BindCallbacks()
        {
            NightStateMachine.On<RunningState>(OnGameRun, gameObject);
            NightStateMachine.On<WinState>(OnGameWin, gameObject);
            NightStateMachine.On<LoseState>(OnGameLose, gameObject);
            NightStateMachine.On<WinState, LoseState>(OnGameFinish, gameObject);
        }
        
        protected virtual void OnAwake() { }
        protected virtual void OnGameRun() { }
        protected virtual void OnGameWin() { }
        protected virtual void OnGameLose() { }
        protected virtual void OnGameFinish() { }
    }
}