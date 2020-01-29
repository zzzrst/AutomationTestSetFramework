using AutomationTestSetFramework;
using System;
using System.Collections.Generic;

namespace AutomationTestSetFrameworkNUnit
{
    class FakeReporter : IReporter
    {
        public List<ITestSetStatus> TestSetStatuses { get; set; }

        public List<ITestCaseStatus> TestCaseStatuses { get; set; }

        public Dictionary<ITestCaseStatus,List<ITestStepStatus>> TestCaseToTestSteps { get; set; }

        public void AddTestCaseStatus(ITestCaseStatus testCaseStatus)
        {
            TestCaseStatuses.Add(testCaseStatus);
        }

        public void AddTestSetStatus(ITestSetStatus testSetStatus)
        {
            TestSetStatuses.Add(testSetStatus);
        }

        public void AddTestStepStatusToTestCase(ITestStepStatus testStepStatus, ITestCaseStatus testCaseStatus)
        {
            if (!TestCaseToTestSteps.ContainsKey(testCaseStatus))
            {
                TestCaseToTestSteps.Add(testCaseStatus, new List<ITestStepStatus>());
            }
            TestCaseToTestSteps[testCaseStatus].Add(testStepStatus);   
        }

        public void Report()
        {
            string str;
            foreach (ITestSetStatus testSetStatus in TestSetStatuses)
            {
                str = testSetStatus.RunSuccessful.ToString();
                str = testSetStatus.ErrorStack;
                str = testSetStatus.FriendlyErrorMessage;
                str = testSetStatus.StartTime.ToString();
                str = testSetStatus.EndTime.ToString();
                str = testSetStatus.Description;
                str = testSetStatus.Expected;
                str = testSetStatus.Actual;
            }

            foreach (ITestCaseStatus testCaseStatus in TestCaseStatuses)
            {
                str = testCaseStatus.RunSuccessful.ToString();
                str = testCaseStatus.ErrorStack;
                str = testCaseStatus.FriendlyErrorMessage;
                str = testCaseStatus.StartTime.ToString();
                str = testCaseStatus.EndTime.ToString();
                str = testCaseStatus.Description;
                str = testCaseStatus.Expected;
                str = testCaseStatus.Actual;

                if (TestCaseToTestSteps.ContainsKey(testCaseStatus))
                {
                    foreach(ITestStepStatus testStepStatus in TestCaseToTestSteps[testCaseStatus])
                    {
                        str = testStepStatus.RunSuccessful.ToString();
                        str = testStepStatus.ErrorStack;
                        str = testStepStatus.FriendlyErrorMessage;
                        str = testStepStatus.StartTime.ToString();
                        str = testStepStatus.EndTime.ToString();
                        str = testStepStatus.Description;
                        str = testStepStatus.Expected;
                        str = testStepStatus.Actual;
                    }
                }
            }
        }

    }
}
