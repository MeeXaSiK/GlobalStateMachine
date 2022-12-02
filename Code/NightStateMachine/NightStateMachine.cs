// ------------------------------------------------------------------------------------
// The MIT License
// NightStateMachine is an object pool for Unity https://github.com/MeeXaSiK/NightStateMachine
// Copyright (c) 2022 Night Train Code
// ------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NTC.Global.System;
using UnityEngine;

namespace NTC.Global.StateMachine
{
    public static class NightStateMachine
    {
        public static GameState LastGameState { get; private set; }

        private static readonly List<SubscribersData> SubscribersMap = 
            new List<SubscribersData>(32);
        
        private static readonly List<GameState> PushedStates = 
            new List<GameState>(32);
        
        public static void On<TState>(in Action action, GameObject owner = null) where TState : GameState
        {
            var gameStateIndex = GetInfo<TState>.Index;
            var newSubscriber = new Subscriber(owner, action);

            if (TryGetSubscribersData<TState>(out var data) == false)
            {
                data = new SubscribersData(gameStateIndex);

                SubscribersMap.Add(data);
            }
            
            data.Subscribers.Add(newSubscriber);
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

        public static void Push<TState>() where TState : GameState, new()
        {
            Push(new TState());
        }
        
        public static void Push<TState>(TState gameState) where TState : GameState
        {
            if (IsStatePossible(gameState) == false)
                return;

            LastGameState = gameState;
            PushedStates.Add(gameState);
            
            Execute<TState>();
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

        public static void RemoveSubscriber(GameObject toRemove)
        {
            if (toRemove == null)
            {
                throw new NullReferenceException(nameof(toRemove), null);
            }

            for (var i = 0; i < SubscribersMap.Count; i++)
            {
                for (var j = 0; j < SubscribersMap[i].Subscribers.Count; j++)
                {
                    if (SubscribersMap[i].Subscribers[j].Owner == toRemove)
                    {
                        SubscribersMap[i].Subscribers.RemoveAt(j);
                    }
                }
            }
        }
        
        public static void Reset()
        {
            LastGameState = null;
            
            SubscribersMap.Clear();
            PushedStates.Clear();
        }

        private static bool TryGetSubscribersData<TState>(out SubscribersData data) where TState : GameState
        {
            var gameStateIndex = GetInfo<TState>.Index;
            
            for (var i = 0; i < SubscribersMap.Count; i++)
            {
                if (SubscribersMap[i].Index == gameStateIndex)
                {
                    data = SubscribersMap[i];
                    return true;
                }
            }

            data = default;
            return false;
        }
        
        private static bool IsStatePossible<TState>(TState gameState) where TState : GameState
        {
            if (LastGameState == null)
                return true;

            if (gameState.CanRepeat == false && WasPushed<TState>())
                return false;

            return true;
        }

        private static void Execute<TState>() where TState : GameState
        {
            if (TryGetSubscribersData<TState>(out var data) == false)
            {
                return;                
            }
            
            for (var i = 0; i < data.Subscribers.Count; i++)
            {
                if (data.Subscribers[i].Action == null)
                {
                    data.Subscribers.RemoveAt(i);
                }
                else
                {
                    data.Subscribers[i].Action.Invoke();
                }
            }
        }
    }
}