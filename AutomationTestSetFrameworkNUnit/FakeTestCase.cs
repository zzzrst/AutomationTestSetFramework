using AutomationTestSetFramework;
using System;
using System.Collections.Generic;
using System.Text;
using static AutomationTestSetFramework.IMethodBoundaryAspect;

namespace AutomationTestSetFrameworkNUnit
{
    public class FakeTestCase : ITestCase
    {
        public bool ShouldExecuteVariable { get; set; }

        public string Name { get; set; }

        public List<ITestStep> TestStep { get; set; }

        private int TestStepIndex = 0;

        public ITestStepStatus TestStepStatus { get; set; }

        public int ExecuteCount { get; private set; } = 0;

        public int ExceptionHandleCount { get; private set; } = 0;

        public FlowBehavior OnExceptionFlowBehavior { get; set; }

        public int SetupCount { get; private set; } = 0;

        public int TearDownCount { get; private set; } = 0;

        public bool NextRunRaiseException { get; set; } = false;
        public int TotalTestSteps { get; set; }

        public ITestCaseStatus TestCaseStatus { get; set; }

        public void Execute()
        {
            ExecuteCount += 1;
            if (NextRunRaiseException)
            {
                throw new Exception();
            }
        }

        public bool ExistNextTestStep()
        {
            return TestStepIndex < TotalTestSteps;
        }

        public ITestStep GetNextTestStep()
        {
            ITestStep teststep = TestStep[TestStepIndex];
            TestStepIndex += 1;
            return teststep;
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
            return this.ShouldExecuteVariable;
        }

        public void TearDown()
        {
            TearDownCount += 1;
        }

        public void UpdateTestCaseStatus(ITestStepStatus testStepStatus)
        {
            throw new NotImplementedException();
        }
    }
}
