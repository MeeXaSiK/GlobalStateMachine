using System;
using System.Collections.Generic;
using UnityEngine;

namespace NTC.Global.StateMachine
{
    public static class NightStateMachine
    {
        private static readonly Dictionary<Type, List<Subscriber>> SubscribersMap = 
            new Dictionary<Type, List<Subscriber>>(32);
        
        private static readonly List<GameState> PushedStates = 
            new List<GameState>(32);

        private static GameState _lastGameState;

        public static void On<TState>(in Action action, GameObject owner = null) where TState : GameState
        {
            var type = typeof(TState);
            var status = SubscribersMap.ContainsKey(type);

            if (status == false)
            {
                SubscribersMap[type] = new List<Subscriber>();
            }

            SubscribersMap[type].Add(new Subscriber(owner, action));
        }

        public static void On<TState1, TState2>(in Action action, GameObject owner = null) 
            where TState1 : GameState
            where TState2 : GameState
        {
            On<TState1>(action, owner);
            On<TState2>(action, owner);
        }
        
        public static void On<TState1, TState2, TState3>(in Action action, GameObject owner = null) 
            where TState1 : GameState
            where TState2 : GameState
            where TState3 : GameState
        {
            On<TState1>(action, owner);
            On<TState2>(action, owner);
            On<TState3>(action, owner);
        }
        
        public static void On<TState1, TState2, TState3, TState4>(in Action action, GameObject owner = null) 
            where TState1 : GameState
            where TState2 : GameState
            where TState3 : GameState
            where TState4 : GameState
        {
            On<TState1>(action, owner);
            On<TState2>(action, owner);
            On<TState3>(action, owner);
            On<TState4>(action, owner);
        }
        
        public static void Push(GameState gameState)
        {
            if (IsStatePossible(gameState) == false)
                return;

            _lastGameState = gameState;
            
            PushedStates.Add(gameState);
            Execute(gameState.GetType());
        }

        public static bool WasPushed<TState>() where TState : GameState
        {
            var count = PushedStates.Count;

            for (var i = 0; i < count; i++)
            {
                if (PushedStates[i].Is<TState>())
                {
                    return true;
                }
            }

            return false;
        }

        public static void Reset()
        {
            SubscribersMap.Clear();
            PushedStates.Clear();
        }

        private static bool IsStatePossible(GameState gameState)
        {
            if (_lastGameState == null)
                return true;

            if (gameState.Is<WinState>() && WasPushed<WinState>())
                return false;
            
            if (gameState.Is<LoseState>() && WasPushed<LoseState>())
                return false;

            return true;
        }

        private static void Execute(Type type)
        {
            var status = SubscribersMap.ContainsKey(type);

            if (status == false)
                return;
            
            if (SubscribersMap[type] == null)
                return;

            var count = SubscribersMap[type].Count;
            
            for (var i = 0; i < count; i++)
            {
                var subscriber = SubscribersMap[type][i];

                if (subscriber.Action == null)
                {
                    SubscribersMap[type].RemoveAt(i);
                }
                else
                {
                    subscriber.Action.Invoke();
                }
            }
        }
    }
}