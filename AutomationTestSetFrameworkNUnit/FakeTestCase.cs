﻿using AutomationTestSetFramework;
using System;
using System.Collections.Generic;
using System.Text;
using static AutomationTestSetFramework.IMethodBoundaryAspect;

namespace AutomationTestSetFrameworkNUnit
{
    public class FakeTestCase : ITestCase
    {
        public bool ShouldExecuteVariable { get; set; } = true;

        public int ShouldExecuteAmountOfTimes { get; set; } = 1;

        public string Name { get; set; }

        public int TestCaseNumber { get; set; }

        public int CurrTestStepNumber { get; set; }

        public List<ITestStep> TestStep { get; set; }

        private int TestStepIndex = 0;

        public ITestStepStatus TestStepStatus { get; set; }

        public int ExceptionHandleCount { get; private set; } = 0;

        public FlowBehavior OnExceptionFlowBehavior { get; set; }

        public int SetupCount { get; private set; } = 0;

        public int TearDownCount { get; private set; } = 0;

        public bool NextRunRaiseException { get; set; } = false;
        public int TotalTestSteps {
            get => TestStep.Count;
            set => TotalTestSteps = TestStep.Count;
        }
        public ITestCaseStatus TestCaseStatus { get; set; }

        public bool ExistNextTestStep()
        {
            if (NextRunRaiseException)
            {
                throw new Exception();
            }
            return TestStepIndex < TotalTestSteps;
        }

        public ITestStep GetNextTestStep()
        {
            ITestStep teststep = TestStep[TestStepIndex];
            TestStepIndex += 1;
            if (TestStepIndex == TestStep.Count && ShouldExecuteAmountOfTimes > 1)
            {
                TestStepIndex = 0;
                ShouldExecuteAmountOfTimes -= 1;
            }

            return teststep;
        }

        public void HandleException(Exception e)
        {
            ExceptionHandleCount += 1;
            // if an exception is thrown, we are recalling the test case. hence it has been executed once.
            ShouldExecuteAmountOfTimes -= 1;
        }

        public void SetUp()
        {
            SetupCount += 1;
        }

        public bool ShouldExecute()
        {
            return this.ShouldExecuteVariable && ShouldExecuteAmountOfTimes > 0;
        }

        public void TearDown()
        {
            TearDownCount += 1;
        }

        public void UpdateTestCaseStatus(ITestStepStatus testStepStatus)
        {
            //
        }
    }
}
