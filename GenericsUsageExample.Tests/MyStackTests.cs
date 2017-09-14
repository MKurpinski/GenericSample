using System.Collections.Generic;
using System.Linq;
using GenericsUsageExample.Models;
using GenericsUsageExample.Stack;
using NUnit.Framework;

namespace GenericsUsageExample.Tests
{
    [TestFixture]
    public class MyStackTests
    {
        [Test]
        public void NewlyCreatedStack_ShouldBeEmpty()
        {
            IMyStack<int> stack = new MyStack<int>();

            Assert.AreEqual(0, stack.Count);
            Assert.IsTrue(stack.IsEmpty);
        }

        [Test]
        public void StackCreatedWithCollection_ShouldHaveCountEqualToCollectionCount()
        {
            var collection = new List<int>{ 1, 2, 3};
            IMyStack<int> stack = new MyStack<int>(collection);

            Assert.AreEqual(collection.Count, stack.Count);
            Assert.IsFalse(stack.IsEmpty);
        }

        [Test]
        public void StackCreatedWithCollection_ShouldBeEmpty_WhenCollectionIsNull()
        {
            IList<int> collection = null;
            IMyStack<int> stack = new MyStack<int>(collection);

            Assert.IsTrue(stack.IsEmpty);
            Assert.AreEqual(0, stack.Count);
        }

        [Test]
        public void StackCreatedWithCollection_ShouldHaveFirstElemEqualToLastElemOfCollection()
        {
            var collection = new List<int> { 1, 2, 3 };
            IMyStack<int> stack = new MyStack<int>(collection);

            var result = stack.Pop();

            Assert.AreEqual(collection.LastOrDefault(), result.Value);
        }

        [Test]
        public void Push_ShouldIncreaseCount()
        {
            IMyStack<Person> stack = new MyStack<Person>();

            var countBefore = stack.Count;
            stack.Push(new Person());
            var countAfter = stack.Count;

            Assert.AreEqual(countBefore + 1, countAfter);
        }

        [Test]
        public void Pop_ShouldReturnFailure_WhenStackIsEmpty()
        {
            IMyStack<int> stack = new MyStack<int>();

            var result = stack.Pop();

            Assert.AreEqual(1, result.Errors.Count);
            Assert.IsFalse(result.Succedeed);
        }

        [Test]
        public void Pop_ShouldReturnSuccess_WhenStackIsNotEmpty()
        {
            IMyStack<int> stack = new MyStack<int>();
            var expectedValue = 5;
            stack.Push(expectedValue);

            var result = stack.Pop();

            Assert.IsTrue(result.Succedeed);
            Assert.AreEqual(expectedValue, result.Value);
        }

        [Test]
        public void Pop_ShouldDecreaseCount_WhenStackIsNotEmpty()
        {
            IMyStack<int> stack = new MyStack<int>();
            stack.Push(2);

            var countBefore = stack.Count;
            stack.Pop();
            var countAfter = stack.Count;

            Assert.AreEqual(countBefore -1, countAfter);
        }

        [Test]
        public void Pop_ShouldNotDecreaseCount_WhenStackIsEmpty()
        {
            IMyStack<Person> stack = new MyStack<Person>();

            var countBefore = stack.Count;
            stack.Pop();
            var countAfter = stack.Count;

            Assert.AreEqual(countBefore, countAfter);
        }

        [Test]
        public void Peek_ShouldReturnSuccess_WhenStackIsNotEmpty()
        {
            IMyStack<int> stack = new MyStack<int>();
            var expectedValue = 5;
            stack.Push(expectedValue);

            var result = stack.Peek();

            Assert.IsTrue(result.Succedeed);
            Assert.AreEqual(expectedValue, result.Value);
        }

        [Test]
        public void Peek_ShouldReturnFailure_WhenStackIsEmpty()
        {
            IMyStack<int> stack = new MyStack<int>();

            var result = stack.Peek();

            Assert.AreEqual(1, result.Errors.Count);
            Assert.IsFalse(result.Succedeed);
        }

        [Test]
        public void Peek_ShouldNotDecreaseCount_WhenStackIsEmpty()
        {
            IMyStack<Person> stack = new MyStack<Person>();

            var countBefore = stack.Count;
            stack.Peek();
            var countAfter = stack.Count;

            Assert.AreEqual(countBefore, countAfter);
        }

        [Test]
        public void Peek_ShouldNotDecreaseCount_WhenStackIsNotEmpty()
        {
            IMyStack<Person> stack = new MyStack<Person>();
            stack.Push(new Person());

            var countBefore = stack.Count;
            stack.Peek();
            var countAfter = stack.Count;

            Assert.AreEqual(countBefore, countAfter);
        }
    }
}
