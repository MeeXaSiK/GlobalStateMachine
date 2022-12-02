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
            
            GlobalStateMachine.Push(stateOnStart);
        }

        private void OnDestroy()
        {
            GlobalStateMachine.Reset();
        }
    }
}