namespace NTC.Global.StateMachine
{
    public abstract class GameState
    {
        public virtual bool CanRepeat => true;
        
        public bool Is<T>() where T : GameState
        {
            return GetType() == typeof(T);
        }
    }
}