using AutomationTestSetFramework;
using System;
using System.Linq;

namespace AutomationTestSetFrameworkNUnit
{
    class FakeTestSetLogger : ITestSetLogger
    {
        private static string Tab(int indents = 1)
        {
            return string.Concat(Enumerable.Repeat("    ", indents));
        }

        public void Log(ITestSet testSet)
        {
            ITestSetStatus testSetStatus = testSet.TestSetStatus;
            string str;
            str = testSet.Name;
            str = testSet.CurrTestCaseNumber.ToString();
            str = testSet.TotalTestCases.ToString();
            str = testSet.CurrTestCaseNumber.ToString();
            str = testSet.OnExceptionFlowBehavior.ToString();
            str = testSetStatus.RunSuccessful.ToString();
            str = testSetStatus.ErrorStack;
            str = testSetStatus.FriendlyErrorMessage;
            str = testSetStatus.StartTime.ToString();
            str = testSetStatus.EndTime.ToString();
            str = testSetStatus.Description;
            str = testSetStatus.Expected;
            str = testSetStatus.Actual;
        }
    }
}
