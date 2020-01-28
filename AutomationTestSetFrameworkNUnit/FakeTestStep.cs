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
                throw new Exception();
            }
            TestStepStatus.RunSuccessful = true;
        }

        public void HandleException(Exception e)
        {
            ExceptionHandleCount += 1;
            TestStepStatus.RunSuccessful = false;
            TestStepStatus.ErrorStack = e.StackTrace;
            TestStepStatus.FriendlyErrorMessage = e.Message;
        }

        public void SetUp()
        {
            SetupCount += 1;
            TestStepStatus.StartTime = DateTime.Now;
        }

        public bool ShouldExecute()
        {
            return ExecuteCount < this.ShouldExecuteAmountOfTimes;
        }

        public void TearDown()
        {
            TearDownCount += 1;
            TestStepStatus.EndTime = DateTime.Now;
        }
    }
}
