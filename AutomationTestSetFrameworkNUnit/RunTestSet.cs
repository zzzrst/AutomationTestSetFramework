using NUnit.Framework;

namespace AutomationTestSetFrameworkNUnit
{
    public class RunTestSetTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}