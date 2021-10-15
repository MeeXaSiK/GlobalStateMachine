using NTC.Global.Cache;

namespace NTC.Global.StateMachine
{
    public abstract class StateMachineUser : NightCache, INightInit
    {
        private bool installed;
        
        public void Init()
        {
            if (installed) return;
            BindCallbacks();
            OnInit();
            installed = true;
        }

        private void BindCallbacks()
        {
            NightStateMachine.On<RunningState>(OnGameRun, gameObject);
            NightStateMachine.On<WinState>(OnGameWin, gameObject);
            NightStateMachine.On<LoseState>(OnGameLose, gameObject);
        }
        
        protected virtual void OnInit() { }
        protected virtual void OnGameRun() { }
        protected virtual void OnGameWin() { }
        protected virtual void OnGameLose() { }
    }
}