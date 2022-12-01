# StateMachine
>Lightweight State Machine for Unity

## Installation

`1) Install files into your Unity Project`

`2) Add "NightStateMachineEntry" on any GameObject in scene`

## How to use

1. Inherit script from `StateMachineUser` and override the methods you need

```csharp
    public class SendMessageOnWinAndLose : StateMachineUser
    {
        protected override void OnAwake()
        {
            Debug.Log("On Awake");
        }

        protected override void OnGameWin()
        {
            Debug.Log("You Won");
        }
        
        protected override void OnGameLose()
        {
            Debug.Log("You Lose");
        }
        
        protected override void OnGameFinish()
        {
            Debug.Log("You Finished");
        }
    }
```

2. Push state you need

```csharp
NightStateMachine.Push(new WinState());
```

## How to add a new custom state

1. Create a new `class` and inherit one from `GameState`

```csharp
public sealed class CustomState : GameState { }
```

2. Open `StateMachineUser` and write a virtual method for new State 

```csharp
protected virtual void OnCustomState() { }
```

3. Bind callback for new state in method `BindCallbacks()`

```csharp
NightStateMachine.On<CustomState>(OnCustomState, gameObject);
```

4. What should be the end result:

```csharp
    public abstract class StateMachineUser : MonoBehaviour
    {
        private void Awake()
        {
            BindCallbacks();
            
            OnAwake();
        }

        private void BindCallbacks()
        {
            NightStateMachine.On<RunningState>(OnGameRun, gameObject);
            NightStateMachine.On<WinState>(OnGameWin, gameObject);
            NightStateMachine.On<LoseState>(OnGameLose, gameObject);
            NightStateMachine.On<WinState, LoseState>(OnGameFinish, gameObject);
            
            NightStateMachine.On<CustomState>(OnCustomState, gameObject); // <<< Bind Callback For The New Custom State
        }
        
        protected virtual void OnAwake() { }
        protected virtual void OnGameRun() { }
        protected virtual void OnGameWin() { }
        protected virtual void OnGameLose() { }
        protected virtual void OnGameFinish() { }
        
        protected virtual void OnCustomState() { } // <<< Virtual Method For The New Custom State
    }
```
