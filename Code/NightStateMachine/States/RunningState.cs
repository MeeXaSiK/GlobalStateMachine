namespace NTC.Global.StateMachine
{
    public sealed class RunningState : GameState
    {
        public override bool CanRepeat => false;
    }
}