using NUnit.Framework;

namespace todo_app_tests
{
    public class Tests
    {
        [SetUp]
#pragma warning disable S1186 // Methods should not be empty
        public void Setup()
#pragma warning restore S1186 // Methods should not be empty
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}