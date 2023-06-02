using System.Threading;

namespace NTC.GlobalStateMachine
{
    public static class StateIndex<T> where T : GameState
    {
        public static readonly int Index;
        
        static StateIndex()
        {
            Index = Interlocked.Increment(ref StateIndexIncrement.Index);
        }
    }
}