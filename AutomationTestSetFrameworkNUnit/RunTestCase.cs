using NUnit.Framework;

namespace AutomationTestSetFrameworkNUnit
{
    public class RunTestCaseTest
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