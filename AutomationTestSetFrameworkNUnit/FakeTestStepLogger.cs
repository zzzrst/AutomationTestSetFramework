using AutomationTestSetFramework;
using System;
using System.Linq;

namespace AutomationTestSetFrameworkNUnit
{
    class FakeTestStepLogger : ITestStepLogger
    {
        public void Log(ITestStep testStep)
        {
            ITestStepStatus testStepStatus = testStep.TestStepStatus;
            string str;
            str = testStep.Name;
            str = testStep.TestStepNumber.ToString();
            str = testStep.OnExceptionFlowBehavior.ToString();
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
