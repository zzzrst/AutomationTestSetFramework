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
        private ITestStep TestStep_2;

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

            TestStep_2 = new FakeTestStep()
            {
                ShouldExecuteAmountOfTimes = 1,
                Name = "Test Step 2",
                NextRunRaiseException = false,
                OnExceptionFlowBehavior = IMethodBoundaryAspect.FlowBehavior.Return,
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
            Assert.AreEqual(1, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 1.");
        }

        [Test]
        public void RunTestCaseMultipleTestStep()
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestStep fakeTestStep2 = TestStep_2 as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;

            fakeTestCase.TestStep.Add(fakeTestStep2);

            AutomationTestSetDriver.RunTestCase(TestCase);

            Assert.AreEqual(1, fakeTestCase.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestCase.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(1, fakeTestStep.ExecuteCount, "Expected the executed count to be 1.");
            Assert.AreEqual(1, fakeTestStep.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestStep.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(1, fakeTestStep2.ExecuteCount, "Expected the executed count to be 1.");
            Assert.AreEqual(1, fakeTestStep2.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestStep2.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestStep2.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }

        [Test]
        public void RunTestCaseNotExecute()
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;

            fakeTestCase.ShouldExecuteVariable = false;

            AutomationTestSetDriver.RunTestCase(TestCase);

            Assert.AreEqual(1, fakeTestCase.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestCase.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(0, fakeTestStep.ExecuteCount, "Expected the executed count to be 0.");
            Assert.AreEqual(0, fakeTestStep.SetupCount, "Expected the setup count to be 0.");
            Assert.AreEqual(0, fakeTestStep.TearDownCount, "Expected the tear down count to be 0.");
            Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }

        [Test]
        public void RunTestCaseWithExceptionAndDefaultFlow() 
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;

            fakeTestCase.NextRunRaiseException = true;

            AutomationTestSetDriver.RunTestCase(TestCase);

            Assert.AreEqual(1, fakeTestCase.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestCase.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(1, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 1.");

            Assert.AreEqual(0, fakeTestStep.ExecuteCount, "Expected the executed count to be 0.");
            Assert.AreEqual(0, fakeTestStep.SetupCount, "Expected the setup count to be 0.");
            Assert.AreEqual(0, fakeTestStep.TearDownCount, "Expected the tear down count to be 0.");
            Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }

        [Test]
        public void RunTestCaseWithExceptionAndRethrowFlow() 
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;

            fakeTestCase.NextRunRaiseException = true;
            fakeTestCase.OnExceptionFlowBehavior = IMethodBoundaryAspect.FlowBehavior.RethrowException;

            Assert.Throws<Exception>(
                () => AutomationTestSetDriver.RunTestCase(TestCase)
            );

            Assert.AreEqual(1, fakeTestCase.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(0, fakeTestCase.TearDownCount, "Expected the tear down count to be 0.");
            Assert.AreEqual(1, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 1.");

            Assert.AreEqual(0, fakeTestStep.ExecuteCount, "Expected the executed count to be 0.");
            Assert.AreEqual(0, fakeTestStep.SetupCount, "Expected the setup count to be 0.");
            Assert.AreEqual(0, fakeTestStep.TearDownCount, "Expected the tear down count to be 0.");
            Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }

        [Test]
        public void RunTestCaseMultipleWithExceptionAndContinueFlow() 
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;

            fakeTestCase.ShouldExecuteAmountOfTimes = 5;
            fakeTestCase.NextRunRaiseException = true;
            fakeTestCase.OnExceptionFlowBehavior = IMethodBoundaryAspect.FlowBehavior.Continue;

            try
            {
                AutomationTestSetDriver.RunTestCase(TestCase);
            }
            catch (Exception)
            {
            }

            Assert.AreEqual(6, fakeTestCase.SetupCount, "Expected the setup count to be 6.");
            Assert.AreEqual(1, fakeTestCase.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(5, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 5.");

            Assert.AreEqual(0, fakeTestStep.ExecuteCount, "Expected the executed count to be 0.");
            Assert.AreEqual(0, fakeTestStep.SetupCount, "Expected the setup count to be 0.");
            Assert.AreEqual(0, fakeTestStep.TearDownCount, "Expected the tear down count to be 0.");
            Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }

        [Test]
        public void RunTestCaseMultipleWithExceptionAndDefaultFlow() 
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;

            fakeTestCase.ShouldExecuteAmountOfTimes = 5;
            fakeTestCase.NextRunRaiseException = true;

            AutomationTestSetDriver.RunTestCase(TestCase);

            Assert.AreEqual(1, fakeTestCase.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestCase.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(1, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 1.");

            Assert.AreEqual(0, fakeTestStep.ExecuteCount, "Expected the executed count to be 0.");
            Assert.AreEqual(0, fakeTestStep.SetupCount, "Expected the setup count to be 0.");
            Assert.AreEqual(0, fakeTestStep.TearDownCount, "Expected the tear down count to be 0.");
            Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }

    }
}