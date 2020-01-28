using System;
using System.Collections.Generic;
using AutomationTestSetFramework;
using NUnit.Framework;

namespace AutomationTestSetFrameworkNUnit
{
    public class RunTestStepTest
    {
        private ITestStep TestStep;
        private ITestStepStatus TestStatus;

        [SetUp]
        public void Setup()
        {
            TestStatus = new FakeTestStepStatus()
            {
                RunSuccessful = false,
                ErrorStack = string.Empty,
                FriendlyErrorMessage = string.Empty,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Description = "Fake Test Step",
                Expected = string.Empty,
                Actual = string.Empty,
                TestStepNumber = 1,
            };
            TestStep = new FakeTestStep()
            {
                ShouldExecuteAmountOfTimes = 1,
                Name = "Test Step",
                TestStepNumber = 1,
                TestStepStatus = TestStatus,
                NextRunRaiseException = false,
                OnExceptionFlowBehavior = IMethodBoundaryAspect.FlowBehavior.Return,
            };
        }

        [Test]
        public void RunTestStepExecuteOnce()
        {
            AutomationTestSetDriver.RunTestStep(TestStep);

            if (TestStep is FakeTestStep fakeTestStep)
            {
                Assert.AreEqual(1, fakeTestStep.ExecuteCount, "Expected the executed count to be 1.");
                Assert.AreEqual(1, fakeTestStep.SetupCount, "Expected the setup count to be 1.");
                Assert.AreEqual(1, fakeTestStep.TearDownCount, "Expected the tear down count to be 1.");
                Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
                Assert.IsTrue(fakeTestStep.TestStepStatus.RunSuccessful, "Expected the test to have ran successfuly");
            }
        }

        [Test]
        public void RunTestStepExecuteWithExceptionAndDefaultFlow()
        {
            if (TestStep is FakeTestStep fakeTestStep)
            {
                fakeTestStep.NextRunRaiseException = true;

                AutomationTestSetDriver.RunTestStep(TestStep);

                Assert.AreEqual(1, fakeTestStep.ExecuteCount, "Expected the executed count to be 1.");
                Assert.AreEqual(1, fakeTestStep.SetupCount, "Expected the setup count to be 1.");
                Assert.AreEqual(1, fakeTestStep.TearDownCount, "Expected the tear down count to be 1.");
                Assert.AreEqual(1, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 1.");
                Assert.IsNotEmpty(fakeTestStep.TestStepStatus.FriendlyErrorMessage, "Expected a friendly error message.");
                Assert.IsNotEmpty(fakeTestStep.TestStepStatus.ErrorStack, "Expected a stack trace.");
            }
        }

        [Test]
        public void RunTestStepExecuteWithExceptionAndRethrowFlow()
        {
            if (TestStep is FakeTestStep fakeTestStep)
            {
                fakeTestStep.NextRunRaiseException = true;
                TestStep.OnExceptionFlowBehavior = IMethodBoundaryAspect.FlowBehavior.RethrowException;

                Assert.Throws<Exception>(
                    () =>   AutomationTestSetDriver.RunTestStep(TestStep)
                );

                Assert.AreEqual(1, fakeTestStep.ExecuteCount, "Expected the executed count to be 1.");
                Assert.AreEqual(1, fakeTestStep.SetupCount, "Expected the setup count to be 1.");
                Assert.AreEqual(0, fakeTestStep.TearDownCount, "Expected the tear down count to be 0.");
                Assert.AreEqual(1, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 1.");
                Assert.IsNotEmpty(fakeTestStep.TestStepStatus.FriendlyErrorMessage, "Expected a friendly error message.");
                Assert.IsNotEmpty(fakeTestStep.TestStepStatus.ErrorStack, "Expected a stack trace.");
            }
        }

        [Test]
        public void RunTestStepExecuteWithMultipleRetries()
        {
            if (TestStep is FakeTestStep fakeTestStep)
            {
                fakeTestStep.ShouldExecuteAmountOfTimes = 5;

                AutomationTestSetDriver.RunTestStep(TestStep);

                Assert.AreEqual(5, fakeTestStep.ExecuteCount, "Expected the executed count to be 5.");
                Assert.AreEqual(1, fakeTestStep.SetupCount, "Expected the setup count to be 1.");
                Assert.AreEqual(1, fakeTestStep.TearDownCount, "Expected the tear down count to be 1.");
                Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
                Assert.IsTrue(fakeTestStep.TestStepStatus.RunSuccessful, "Expected the test to have ran successfuly");
            }
        }

        [Test]
        public void RunTestStepExecuteWithMultipleRetriesWithExceptionAndDefaultFlow()
        {
            if (TestStep is FakeTestStep fakeTestStep)
            {
                fakeTestStep.ShouldExecuteAmountOfTimes = 5;
                fakeTestStep.NextRunRaiseException = true;

                AutomationTestSetDriver.RunTestStep(TestStep);

                Assert.AreEqual(1, fakeTestStep.ExecuteCount, "Expected the executed count to be 1.");
                Assert.AreEqual(1, fakeTestStep.SetupCount, "Expected the setup count to be 1.");
                Assert.AreEqual(1, fakeTestStep.TearDownCount, "Expected the tear down count to be 1.");
                Assert.AreEqual(1, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 1.");
                Assert.IsNotEmpty(fakeTestStep.TestStepStatus.FriendlyErrorMessage, "Expected a friendly error message.");
                Assert.IsNotEmpty(fakeTestStep.TestStepStatus.ErrorStack, "Expected a stack trace.");
            }
        }

        [Test]
        public void RunTestStepExecuteWithMultipleRetriesWithExceptionAndContinueFlow()
        {
            if (TestStep is FakeTestStep fakeTestStep)
            {
                fakeTestStep.ShouldExecuteAmountOfTimes = 5;
                fakeTestStep.NextRunRaiseException = true;
                fakeTestStep.OnExceptionFlowBehavior = IMethodBoundaryAspect.FlowBehavior.Continue;

                try
                {
                    AutomationTestSetDriver.RunTestStep(TestStep);
                }
                catch (Exception)
                {
                }

                Assert.AreEqual(5, fakeTestStep.ExecuteCount, "Expected the executed count to be 5.");
                Assert.AreEqual(6, fakeTestStep.SetupCount, "Expected the setup count to be 6.");
                Assert.AreEqual(1, fakeTestStep.TearDownCount, "Expected the tear down count to be 5.");
                Assert.AreEqual(5, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 1.");
                Assert.IsNotEmpty(fakeTestStep.TestStepStatus.FriendlyErrorMessage, "Expected a friendly error message.");
                Assert.IsNotEmpty(fakeTestStep.TestStepStatus.ErrorStack, "Expected a stack trace.");
            }
        }
    }
}