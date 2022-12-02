namespace NTC.GlobalStateMachine
{
    public sealed class WinState : GameState
    {
        public override bool CanRepeat => false;

        protected override void BlockNextStates()
        {
            BlockNextState<RunningState>();
            BlockNextState<LoseState>();
        }
    }
}