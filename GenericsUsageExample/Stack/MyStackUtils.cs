using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsUsageExample.Stack
{
    public static class MyStackUtils
    {
        public static MyStack<T> CreateStackWithDefaultValues<T>(int numberOfValues) where T : new()
        {
            var stack = new MyStack<T>();
            if (numberOfValues > 0)
            {
                for (var i = 0; i < numberOfValues; i++)
                {
                    stack.Push(new T());
                }
            }
            return stack;
        }
    }
}
