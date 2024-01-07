namespace Shuttle.Core.Contract
{
    public class DisposedEventArgs<T> where T : class
    {
        public T Instance { get; }

        public DisposedEventArgs(T instance)
        {
            Instance = Guard.AgainstNull(instance, nameof(instance));
        }
    }
}