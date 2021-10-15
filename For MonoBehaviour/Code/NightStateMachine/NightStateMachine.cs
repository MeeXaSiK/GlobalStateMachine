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

        private static GameState lastGameState;

        public static void On<TState>(Action action, GameObject owner = null) where TState : GameState
        {
            var type = typeof(TState);
            var status = SubscribersMap.ContainsKey(type);

            if (!status)
                SubscribersMap[type] = new List<Subscriber>();

            SubscribersMap[type].Add(new Subscriber(owner, action));
        }

        public static void On<TState1, TState2>(Action action, GameObject owner = null) 
            where TState1 : GameState
            where TState2 : GameState
        {
            On<TState1>(action, owner);
            On<TState2>(action, owner);
        }
        
        public static void On<TState1, TState2, TState3>(Action action, GameObject owner = null) 
            where TState1 : GameState
            where TState2 : GameState
            where TState3 : GameState
        {
            On<TState1>(action, owner);
            On<TState2>(action, owner);
            On<TState3>(action, owner);
        }
        
        public static void Push(GameState gameState)
        {
            if (!IsStatePossible(gameState))
                return;

            lastGameState = gameState;
            
            PushedStates.Add(gameState);
            Execute(gameState.GetType());
        }

        public static bool WasPushed<TState>() where TState : GameState
        {
            var count = PushedStates.Count;
            
            for (var i = 0; i < count; i++)
                if (PushedStates[i].Is<TState>()) return true;

            return false;
        }
        
        public static int Count<TState>() where TState : GameState
        {
            var count = 0;
            
            foreach (GameState gameState in PushedStates)
                if (gameState.Is<TState>()) count++;

            return count;
        }

        public static void Reset()
        {
            SubscribersMap.Clear();
            PushedStates.Clear();
        }

        private static bool IsStatePossible(GameState gameState)
        {
            if (lastGameState == null)
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

            if (!status) return;
            if (SubscribersMap[type] == null) return;

            var count = SubscribersMap[type].Count;
            
            for (var i = 0; i < count; i++)
            {
                var subscriber = SubscribersMap[type][i];
                
                if (subscriber.Action == null) SubscribersMap[type].RemoveAt(i);
                else subscriber.Action.Invoke();
            }
        }
    }
}