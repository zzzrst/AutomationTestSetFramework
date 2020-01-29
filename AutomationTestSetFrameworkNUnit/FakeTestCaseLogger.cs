using AutomationTestSetFramework;
using System;
using System.Linq;

namespace AutomationTestCaseFrameworkNUnit
{
    class FakeTestCaseLogger : ITestCaseLogger
    {
        public void Log(ITestCase testCase)
        {
            ITestCaseStatus testCaseStatus = testCase.TestCaseStatus;
            string str;
            str = testCase.CurrTestStepNumber.ToString();
            str = testCase.Name;
            str = testCase.OnExceptionFlowBehavior.ToString();
            str = testCase.TestCaseNumber.ToString();
            str = testCase.TotalTestSteps.ToString();
            str = testCaseStatus.RunSuccessful.ToString();
            str = testCaseStatus.ErrorStack;
            str = testCaseStatus.FriendlyErrorMessage;
            str = testCaseStatus.StartTime.ToString();
            str = testCaseStatus.EndTime.ToString();
            str = testCaseStatus.Description;
            str = testCaseStatus.Expected;
            str = testCaseStatus.Actual;
        }
    }
}
