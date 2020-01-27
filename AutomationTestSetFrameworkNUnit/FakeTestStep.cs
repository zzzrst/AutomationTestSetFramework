using AutomationTestSetFramework;
using System;
using System.Collections.Generic;
using System.Text;
using static AutomationTestSetFramework.IMethodBoundaryAspect;

namespace AutomationTestSetFrameworkNUnit
{
    public class FakeTestStep : ITestStep
    {
        public int ShouldExecuteAmountOfTimes { get; set; }

        public string Name { get; set; }

        public int TestStepNumber { get; set; }

        public ITestStepStatus TestStepStatus { get; set; }

        public int ExecuteCount { get; private set; } = 0;

        public int ExceptionHandleCount { get; private set; } = 0;

        public int SetupCount { get; private set; } = 0;

        public int TearDownCount { get; private set; } = 0;

        public bool NextRunRaiseException { get; set; } = false;

        public FlowBehavior OnExceptionFlowBehavior { get; set; }

        public void Execute()
        {
            ExecuteCount += 1;
            if (NextRunRaiseException)
            {
                TestStepStatus.RunSuccessful = false;
                throw new Exception();
            }
            TestStepStatus.RunSuccessful = true;
        }

        public void HandleException(Exception e)
        {
            ExceptionHandleCount += 1;
        }

        public void SetUp()
        {
            SetupCount += 1;
        }

        public bool ShouldExecute()
        {
            return ExecuteCount < this.ShouldExecuteAmountOfTimes;
        }

        public void TearDown()
        {
            TearDownCount += 1;
        }
    }
}
