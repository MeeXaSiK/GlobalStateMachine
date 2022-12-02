using UnityEngine;

namespace NTC.GlobalStateMachine
{
    public sealed class GlobalStateMachineEntry : MonoBehaviour
    {
        [SerializeField] private GameStates stateOnStart = GameStates.None;
        
        private void Start()
        {
            if (stateOnStart == GameStates.None)
                return;
            
            var newState = stateOnStart.GetState();

            GlobalStateMachine.Push(newState);
        }

        private void OnDestroy()
        {
            GlobalStateMachine.Reset();
        }
    }
}