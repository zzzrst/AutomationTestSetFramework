using AutomationTestSetFramework;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTestSetFrameworkNUnit
{
    public class RunTestCaseTest
    {
        private ITestCase TestCase;
        private ITestStep TestStep;

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
        public void RunTestCaseExecuteOnce()
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;

            AutomationTestSetDriver.RunTestCase(TestCase);

            Assert.AreEqual(1, fakeTestCase.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestCase.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(1, fakeTestStep.ExecuteCount, "Expected the executed count to be 1.");
            Assert.AreEqual(1, fakeTestStep.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestStep.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }

        [Test]
        public void RunTestCaseExecuteMultiple()
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;

            fakeTestCase.ShouldExecuteAmountOfTimes = 3;
            
            AutomationTestSetDriver.RunTestCase(TestCase);

            Assert.AreEqual(1, fakeTestCase.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestCase.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(1, fakeTestStep.ExecuteCount, "Expected the executed count to be 1.");
            Assert.AreEqual(3, fakeTestStep.SetupCount, "Expected the setup count to be 3.");
            Assert.AreEqual(3, fakeTestStep.TearDownCount, "Expected the tear down count to be 3.");
            Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }

        [Test]
        public void RunTestCaseExecuteOnceTestStepException()
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;

            fakeTestStep.NextRunRaiseException = true;
            AutomationTestSetDriver.RunTestCase(TestCase);

            Assert.AreEqual(1, fakeTestCase.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestCase.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(1, fakeTestStep.ExecuteCount, "Expected the executed count to be 1.");
            Assert.AreEqual(1, fakeTestStep.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestStep.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(1, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }
    }
}