using GenericsUsageExample.Models;
using GenericsUsageExample.Stack;
using NUnit.Framework;

namespace GenericsUsageExample.Tests
{
    [TestFixture]
    public class MyStackUtilsStack
    {
        [Test]
        public void CreateStackWithDefaultValues_ShouldCreateStackWithDefaultValues_WhenNumberGreaterThanZero()
        {
            var number = 1;
            var stack = MyStackUtils.CreateStackWithDefaultValues<Person>(number);

            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(number, stack.Count);
        }

        [Test]
        public void CreateStackWithDefaultValues_ShouldCreateEmptyStack_WhenNumberEqualZero()
        {
            var number = 0;
            var stack = MyStackUtils.CreateStackWithDefaultValues<Person>(number);

            Assert.IsTrue(stack.IsEmpty);
            Assert.AreEqual(number, stack.Count);
        }

        [Test]
        public void CreateStackWithDefaultValues_ShouldCreateEmptyStack_WhenNumberLessThanZero()
        {
            var number = -1;
            var stack = MyStackUtils.CreateStackWithDefaultValues<Person>(number);

            Assert.IsTrue(stack.IsEmpty);
            Assert.AreEqual(0, stack.Count);
        }
    }
}
