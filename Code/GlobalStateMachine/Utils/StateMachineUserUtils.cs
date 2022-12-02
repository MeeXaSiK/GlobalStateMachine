using System;

namespace NTC.GlobalStateMachine
{
    public static class StateMachineUserUtils
    {
        public static void On<TState>(this StateMachineUser stateMachineUser, Action action) where TState : GameState
        {
            GlobalStateMachine.On<TState>(action, stateMachineUser.gameObject);
        }
        
        public static void On<TState1, TState2>(this StateMachineUser stateMachineUser, Action action) 
            where TState1 : GameState
            where TState2 : GameState
        {
            GlobalStateMachine.On<TState1, TState2>(action, stateMachineUser.gameObject);
        }
        
        public static void On<TState1, TState2, TState3>(this StateMachineUser stateMachineUser, Action action) 
            where TState1 : GameState
            where TState2 : GameState
            where TState3 : GameState
        {
            GlobalStateMachine.On<TState1, TState2, TState3>(action, stateMachineUser.gameObject);
        }
        
        public static void On<TState1, TState2, TState3, TState4>(this StateMachineUser stateMachineUser, Action action) 
            where TState1 : GameState
            where TState2 : GameState
            where TState3 : GameState
            where TState4 : GameState
        {
            GlobalStateMachine.On<TState1, TState2, TState3, TState4>(action, stateMachineUser.gameObject);
        }
        
        public static void RemoveSubscriber(this StateMachineUser stateMachineUser)
        {
            GlobalStateMachine.RemoveSubscriber(stateMachineUser.gameObject);
        }
    }
}