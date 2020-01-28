using AutomationTestSetFramework;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTestSetFrameworkNUnit
{
    public class RunTestSetTest
    {
        private ITestCase TestCase;
        private ITestStep TestStep;
        private ITestSet TestSet;

        private ITestCase TestCase_2;
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

            TestSet = new FakeTestSet()
            {
                Name = "Test Set",
                OnExceptionFlowBehavior = IMethodBoundaryAspect.FlowBehavior.Return,
                TestCases = new List<ITestCase> { TestCase },
            };

            TestStep_2 = new FakeTestStep()
            {
                ShouldExecuteAmountOfTimes = 1,
                Name = "Test Step 2",
                NextRunRaiseException = false,
                OnExceptionFlowBehavior = IMethodBoundaryAspect.FlowBehavior.Return,
            };

            TestCase_2 = new FakeTestCase()
            {
                ShouldExecuteAmountOfTimes = 1,
                Name = "Test Case 2",
                NextRunRaiseException = false,
                OnExceptionFlowBehavior = IMethodBoundaryAspect.FlowBehavior.Return,
                TestStep = new List<ITestStep>() { TestStep_2 },
            };
        }

        [Test]
        public void RunTestSetExecute()
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;
            FakeTestSet fakeTestSet = TestSet as FakeTestSet;

            AutomationTestSetDriver.RunTestSet(fakeTestSet);

            Assert.AreEqual(1, fakeTestSet.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestSet.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestSet.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(1, fakeTestCase.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestCase.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(1, fakeTestStep.ExecuteCount, "Expected the executed count to be 1.");
            Assert.AreEqual(1, fakeTestStep.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestStep.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }

        [Test]
        public void RunTestSetNotExecute()
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;
            FakeTestSet fakeTestSet = TestSet as FakeTestSet;

            fakeTestSet.ShouldExecuteVariable = false;

            AutomationTestSetDriver.RunTestSet(fakeTestSet);

            Assert.AreEqual(1, fakeTestSet.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestSet.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestSet.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(0, fakeTestCase.SetupCount, "Expected the setup count to be 0.");
            Assert.AreEqual(0, fakeTestCase.TearDownCount, "Expected the tear down count to be 0.");
            Assert.AreEqual(0, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(0, fakeTestStep.ExecuteCount, "Expected the executed count to be 0.");
            Assert.AreEqual(0, fakeTestStep.SetupCount, "Expected the setup count to be 0.");
            Assert.AreEqual(0, fakeTestStep.TearDownCount, "Expected the tear down count to be 0.");
            Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }

        [Test]
        public void RunTestSetExecuteTestStepException()
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;
            FakeTestSet fakeTestSet = TestSet as FakeTestSet;

            fakeTestStep.NextRunRaiseException = true;

            AutomationTestSetDriver.RunTestSet(fakeTestSet);

            Assert.AreEqual(1, fakeTestSet.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestSet.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestSet.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(1, fakeTestCase.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestCase.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(1, fakeTestStep.ExecuteCount, "Expected the executed count to be 1.");
            Assert.AreEqual(1, fakeTestStep.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestStep.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(1, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 1.");
        }
        
        [Test]
        public void RunTestSetMultipleTestCases()
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestStep fakeTestStep2 = TestStep_2 as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;
            FakeTestCase fakeTestCase2 = TestCase_2 as FakeTestCase;
            FakeTestSet fakeTestSet = TestSet as FakeTestSet;

            fakeTestSet.TestCases.Add(fakeTestCase2);

            AutomationTestSetDriver.RunTestSet(fakeTestSet);

            Assert.AreEqual(1, fakeTestSet.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestSet.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestSet.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(1, fakeTestCase.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestCase.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(1, fakeTestStep.ExecuteCount, "Expected the executed count to be 1.");
            Assert.AreEqual(1, fakeTestStep.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestStep.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(1, fakeTestCase2.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestCase2.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestCase2.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(1, fakeTestStep2.ExecuteCount, "Expected the executed count to be 1.");
            Assert.AreEqual(1, fakeTestStep2.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestStep2.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(0, fakeTestStep2.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }

        [Test]
        public void RunTestSetWithExceptionAndDefaultFlow() 
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;
            FakeTestSet fakeTestSet = TestSet as FakeTestSet;

            fakeTestSet.NextRunRaiseException = true;

            AutomationTestSetDriver.RunTestSet(fakeTestSet);

            Assert.AreEqual(1, fakeTestSet.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(1, fakeTestSet.TearDownCount, "Expected the tear down count to be 1.");
            Assert.AreEqual(1, fakeTestSet.ExceptionHandleCount, "Expected the exception handle count to be 1.");

            Assert.AreEqual(0, fakeTestCase.SetupCount, "Expected the setup count to be 0.");
            Assert.AreEqual(0, fakeTestCase.TearDownCount, "Expected the tear down count to be 0.");
            Assert.AreEqual(0, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(0, fakeTestStep.ExecuteCount, "Expected the executed count to be 0.");
            Assert.AreEqual(0, fakeTestStep.SetupCount, "Expected the setup count to be 0.");
            Assert.AreEqual(0, fakeTestStep.TearDownCount, "Expected the tear down count to be 0.");
            Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }

        [Test]
        public void RunTestSetWithExceptionAndRethrowFlow() 
        {
            FakeTestStep fakeTestStep = TestStep as FakeTestStep;
            FakeTestCase fakeTestCase = TestCase as FakeTestCase;
            FakeTestSet fakeTestSet = TestSet as FakeTestSet;

            fakeTestSet.NextRunRaiseException = true;
            fakeTestSet.OnExceptionFlowBehavior = IMethodBoundaryAspect.FlowBehavior.RethrowException;

            Assert.Throws<Exception>(
                () => AutomationTestSetDriver.RunTestSet(fakeTestSet)
            );

            Assert.AreEqual(1, fakeTestSet.SetupCount, "Expected the setup count to be 1.");
            Assert.AreEqual(0, fakeTestSet.TearDownCount, "Expected the tear down count to be 0.");
            Assert.AreEqual(1, fakeTestSet.ExceptionHandleCount, "Expected the exception handle count to be 1.");

            Assert.AreEqual(0, fakeTestCase.SetupCount, "Expected the setup count to be 0.");
            Assert.AreEqual(0, fakeTestCase.TearDownCount, "Expected the tear down count to be 0.");
            Assert.AreEqual(0, fakeTestCase.ExceptionHandleCount, "Expected the exception handle count to be 0.");

            Assert.AreEqual(0, fakeTestStep.ExecuteCount, "Expected the executed count to be 0.");
            Assert.AreEqual(0, fakeTestStep.SetupCount, "Expected the setup count to be 0.");
            Assert.AreEqual(0, fakeTestStep.TearDownCount, "Expected the tear down count to be 0.");
            Assert.AreEqual(0, fakeTestStep.ExceptionHandleCount, "Expected the exception handle count to be 0.");
        }
    }
}