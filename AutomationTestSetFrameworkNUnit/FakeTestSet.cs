using AutomationTestSetFramework;
using System;
using System.Collections.Generic;
using System.Text;
using static AutomationTestSetFramework.IMethodBoundaryAspect;

namespace AutomationTestSetFrameworkNUnit
{
    public class FakeTestSet : ITestSet
    {
        public bool ShouldExecuteVariable { get; set; } = true;

        public bool ExistNextTestCaseVariable { get; set; }

        public string Name { get; set; }
        
        public List<ITestCase> TestCases { get; set; }

        private int testCaseIndex = 0;

        public ITestSetStatus TestSetStatus { get; set; }

        public int CurrTestCaseNumber { get; set; }

        public int ExecuteCount { get; private set; } = 0;

        public int ExceptionHandleCount { get; private set; } = 0;

        public FlowBehavior OnExceptionFlowBehavior { get; set; }

        public int SetupCount { get; private set; } = 0;

        public int TearDownCount { get; private set; } = 0;

        public bool NextRunRaiseException { get; set; } = false;
        public int TotalTestCases
        { 
            get => TestCases.Count;
            set => TotalTestCases = TestCases.Count;
        }

        public bool ExistNextTestCase()
        {
            if (NextRunRaiseException)
            {
                throw new Exception();
            }
            return testCaseIndex < TotalTestCases;
        }

        public ITestCase GetNextTestCase()
        {
            ITestCase tc =  TestCases[testCaseIndex];
            testCaseIndex += 1;
            return tc;
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

        public void UpdateTestSetStatus(ITestCaseStatus testCaseStatus)
        {
            //
        }
    }
}
