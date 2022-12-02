namespace NTC.GlobalStateMachine
{
    public sealed class LoseState : GameState
    {
        public override bool CanRepeat => false;

        protected override void BlockNextStates()
        {
            BlockNextState<RunningState>();
            BlockNextState<WinState>();
        }
    }
}