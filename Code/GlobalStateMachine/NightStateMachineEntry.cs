using UnityEngine;

namespace NTC.Global.StateMachine
{
    public sealed class NightStateMachineEntry : MonoBehaviour
    {
        [SerializeField] private GameStates stateOnStart = GameStates.None;
        
        private void Start()
        {
            if (stateOnStart == GameStates.None)
                return;
            
            var newState = stateOnStart.GetState();

            NightStateMachine.Push(newState);
        }

        private void OnDestroy()
        {
            NightStateMachine.Reset();
        }
    }
}