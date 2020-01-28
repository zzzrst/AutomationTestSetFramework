using AutomationTestSetFramework;
using NUnit.Framework;

namespace AutomationTestSetFrameworkNUnit
{
    public class RunTestSetTest
    {
        private ITestCase TestCase;
        private ITestStep TestStep;
        private ITestSet TestSet;

        [SetUp]
        public void Setup()
        {
            TestStep = new FakeTestStep()
            {
                ShouldExecuteAmountOfTimes = 1,
                Name = "Test Step",
                NextRunRaiseException = false,
                OnExceptionFlowBehavior = IMethodBoundaryAspect.FlowBehavior.Return,
            };

            TestCase = new FakeTestCase()
            {
                ShouldExecuteAmountOfTimes = 1,
                Name = "Test Case",
                NextRunRaiseException = false,
                OnExceptionFlowBehavior = IMethodBoundaryAspect.FlowBehavior.Return,
                TestStep = new List<ITestStep>() { TestStep },
            };
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}