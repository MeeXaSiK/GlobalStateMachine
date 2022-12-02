using System.Collections.Generic;

namespace NTC.GlobalStateMachine
{
    public readonly struct SubscribersData
    {
        public readonly List<Subscriber> Subscribers;
        public readonly int Index;

        public SubscribersData(int gameStateIndex)
        {
            Subscribers = new List<Subscriber>(32);
            Index = gameStateIndex;
        }
    }
}