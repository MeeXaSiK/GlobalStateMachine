using UnityEngine;

namespace NTC.Global.StateMachine
{
    public sealed class NightStateMachineEntry : MonoBehaviour
    {
        private void Awake()
        {
            NightStateMachine.Push(new RunningState());
        }

        private void OnDestroy()
        {
            NightStateMachine.Reset();
        }
    }
}