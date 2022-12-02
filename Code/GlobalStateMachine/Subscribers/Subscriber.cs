using System;
using UnityEngine;

namespace NTC.GlobalStateMachine
{
    public readonly struct Subscriber
    {
        public readonly GameObject Owner;
        public readonly Action Action;

        public Subscriber(GameObject owner, Action action)
        {
            Owner = owner;
            Action = action;
        }
    }
}