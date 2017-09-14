using System.Collections.Generic;

namespace GenericsUsageExample.Stack
{
    public class MyStack<T> : IMyStack<T>
    {
        public bool IsEmpty => _first == null;
        public int Count { get; private set; }
        private const string EMPTY_STACK = "Stack is empty";
        private Node<T> _first;

        public MyStack()
        {
            Count = 0;
        }

        public MyStack(ICollection<T> collection): this()
        {
            if (collection == null)
            {
                return;
            }
            foreach (var elem in collection)
            {
                _first = new Node<T>(elem, _first);
            }
            Count = collection.Count;
        }

        public void Push(T obj)
        {
            _first = _first == null ? new Node<T>(obj) : new Node<T>(obj, _first);
            Count++;
        }

        public Result<T> Pop()
        {
            if (IsEmpty)
            {
                return Result<T>.Failure(EMPTY_STACK);
            }

            var elem = _first.Value;
            _first = _first.Next;
            Count--;
            return Result<T>.Success(elem);
        }

        public Result<T> Peek()
        {
            if (IsEmpty)
            {
                return Result<T>.Failure(EMPTY_STACK);
            }
            return Result<T>.Success(_first.Value);
        }
        private class Node<TE>
        {
            public TE Value { get;}
            public Node<TE> Next { get; }

            public Node(TE value)
            {
                Value = value;
            }

            public Node(TE value, Node<TE> next):this(value)
            {
                Next = next;
            }
        }
    }

}
