using System;
using UnityEngine;

namespace NTC.Global.StateMachine
{
    public struct Subscriber
    {
        public GameObject Owner { get; }
        public Action Action { get; }

        public Subscriber(GameObject owner, Action action)
        {
            Owner = owner;
            Action = action;
        }
    }
}