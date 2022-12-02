using System.Collections.Generic;

namespace NTC.Global.StateMachine
{
    public readonly struct SubscribersData
    {
        public readonly List<Subscriber> Subscribers;
        public readonly int Index;

        public SubscribersData(int gameStateType)
        {
            Subscribers = new List<Subscriber>(32);
            Index = gameStateType;
        }
    }
}