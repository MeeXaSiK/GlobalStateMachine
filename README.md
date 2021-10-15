# NightStateMachine
Lightweight State Machine for Unity

### HOW TO USE

1) Install files into your Unity Project
2) Add "NightStateMachineEntry" on any GameObject in scene

### Using for MonoBehaviour

```sh
    public class StateTest : StateMachineUser
    {
        protected override void OnAwake()
        {
            Debug.Log("On Awake");
        }

        protected override void OnGameLose()
        {
            Debug.Log("You Lose");
        }

        protected override void OnGameWin()
        {
            Debug.Log("You Won");
        }
    }
```

### Using for NightCache System

```sh
    public class StateTest : StateMachineUser, INightRun
    {
        public void Run() 
        {
            Debug.Log("Game is Running");
        }
        
        protected override void OnInit()
        {
            Debug.Log("On Init");
        }

        protected override void OnGameLose()
        {
            Debug.Log("You Lose");
        }

        protected override void OnGameWin()
        {
            Debug.Log("You Won");
        }
    }
```

### How to Push new State

```sh
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                NightStateMachine.Push(new WinState());
            }
        }
```

### You can add new State in GameStates.cs

```sh
    public abstract class GameState
    {
        private Type Type => cachedType ??= GetType();
        private Type cachedType;
        
        public bool Is<T>() where T : GameState => 
            Type == typeof(T);
    }
    
    public sealed class RunningState : GameState { }
    public sealed class WinState : GameState { }
    public sealed class LoseState : GameState { }
    
    public sealed class CustomState : GameState { }
```

### StateMachineUser.cs

```sh
    public abstract class StateMachineUser : MonoBehaviour
    {
        private void Awake()
        {
            BindCallbacks();
            OnAwake();
        }

        private void BindCallbacks()
        {
            NightStateMachine.On<CustomState>(OnCustomState, gameObject);
        
            NightStateMachine.On<RunningState>(OnGameRun, gameObject);
            NightStateMachine.On<WinState>(OnGameWin, gameObject);
            NightStateMachine.On<LoseState>(OnGameLose, gameObject);
        }
        
        protected virtual void OnCustomState() { }
        
        protected virtual void OnAwake() { }
        protected virtual void OnGameRun() { }
        protected virtual void OnGameWin() { }
        protected virtual void OnGameLose() { }
    }
```
