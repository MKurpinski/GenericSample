namespace GenericsUsageExample.Stack
{
    public interface IMyStack<T>
    {
        bool IsEmpty { get; }
        int Count { get; }
        void Push(T obj);
        Result<T> Pop();
        Result<T> Peek();
    }
}
