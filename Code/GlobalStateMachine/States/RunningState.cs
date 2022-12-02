namespace NTC.GlobalStateMachine
{
    public sealed class RunningState : GameState
    {
        public override bool CanRepeat => false;
    }
}