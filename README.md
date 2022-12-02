# Global State Machine

Lightweight State Machine for Unity for global states such as Win or Lose.
* Easy to use
* Supports custom game states
* Awesome performance

## Installation

1. Install files into your Unity Project
2. Add `GlobalStateMachineEntry` on any `GameObject` in scene

## How to use

1. Write using `using NTC.GlobalStateMachine`

2. Inherit script from `StateMachineUser` and override the methods you need

```csharp
    public class Sample : StateMachineUser
    {
        protected override void OnAwake()
        {
            Debug.Log("On Awake");
        }
        
        protected override void OnDestroyOverridable()
        {
            Debug.Log("On Destroy");
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

3. Push state you need

```csharp
GlobalStateMachine.Push(new WinState());
```
```csharp
GlobalStateMachine.Push<WinState>();
```
```csharp
GlobalStateMachine.Push(GameStates.Win);
```

## Examples of using

You can push any state if entity is in the trigger

```csharp
    public class PushWinStateOnPlayerEnter : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerUnit playerUnit))
            {
                GlobalStateMachine.Push(new WinState());
            }
        }
    }
```

Also you can get a new state by extension method `GetState()` of the `GameStates` enum

```csharp
    public class PushStateOnTriggerEnter : MonoBehaviour
    {
        [SerializeField] private GameStates stateToPush = GameStates.Win;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerUnit playerUnit))
            {                
                GlobalStateMachine.Push(stateToPush);
            }
        }
    }
```

For example, you can play audio on game win

```csharp
    public class PlayAudioOnWin : StateMachineUser
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;

        protected override void OnGameWin()
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
```

You can check any state for pushed

```csharp
var isStatePushed = GlobalStateMachine.WasPushed<TState>();
```

## How to add a custom states and methods

1. Create a new `class` and inherit one from `GameState`

```csharp
public sealed class CustomState : GameState { }
```

2. Optionally you can override the `CanRepeat` parameter (default value is `true`)

```csharp
public sealed class CustomState : GameState 
{
    public override bool CanRepeat => false;
}
```

3. Optionally you can block any next states for the `CustomState`

```csharp
public sealed class CustomState : GameState
{
    public override bool CanRepeat => false;
    
    protected override void BlockNextStates()
    {
        BlockNextState<RunningState>();
        BlockNextState<LoseState>();
    }
}
```

4. Open `StateMachineUser` and write a virtual method for new State 

```csharp
protected virtual void OnCustomState() { }
```

5. Bind callback for new state in method `BindCallbacks()`

```csharp
this.On<CustomState>(OnCustomState);
```

6. What should be the end result:

```csharp
    public abstract class StateMachineUser : MonoBehaviour
    {
        private void Awake()
        {
            BindCallbacks();
            
            OnAwake();
        }

        private void OnDestroy()
        {
            this.RemoveSubscriber();
            
            OnDestroyOverridable();
        }

        private void BindCallbacks()
        {
            this.On<RunningState>(OnGameRun);
            this.On<WinState>(OnGameWin);
            this.On<LoseState>(OnGameLose);
            this.On<WinState, LoseState>(OnGameFinish);

            this.On<CustomState>(OnCustomState); // <<< Bind Callback For The New Custom State
        }

        protected virtual void OnAwake() { }
        protected virtual void OnDestroyOverridable() { }
        protected virtual void OnGameRun() { }
        protected virtual void OnGameWin() { }
        protected virtual void OnGameLose() { }
        protected virtual void OnGameFinish() { }
        
        protected virtual void OnCustomState() { } // <<< Virtual Method For The New Custom State
    }
```
