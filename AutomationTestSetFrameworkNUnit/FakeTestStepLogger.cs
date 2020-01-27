using AutomationTestSetFramework;
using System;
using System.Linq;

namespace AutomationTestSetFrameworkNUnit
{
    class FakeTestStepLogger : ITestStepLogger
    {
        private static string Tab(int indents = 1)
        {
            return string.Concat(Enumerable.Repeat("    ", indents));
        }

        public void Log(ITestStep testStep)
        {
            ITestStepStatus testStepStatus = testStep.TestStepStatus;
            Console.WriteLine(Tab(2) + $"Test Step Name: {testStep.Name}.");
            Console.WriteLine(Tab(2) + $"Test Step Number: {testStep.TestStepNumber}.");
            Console.WriteLine(Tab(2) + $"Test Step Status");
            Console.WriteLine(Tab(2) + $"Run Successful: {testStepStatus.RunSuccessful}");
            Console.WriteLine(Tab(2) + $"Error Stack: {testStepStatus.ErrorStack}");
            Console.WriteLine(Tab(2) + $"Friendly Error Message: {testStepStatus.FriendlyErrorMessage}");
            Console.WriteLine(Tab(2) + $"Start Time: {testStepStatus.StartTime}");
            Console.WriteLine(Tab(2) + $"End Time: {testStepStatus.EndTime}");
            Console.WriteLine(Tab(2) + $"Description: {testStepStatus.Description}");
            Console.WriteLine(Tab(2) + $"Expected: {testStepStatus.Expected}");
            Console.WriteLine(Tab(2) + $"Actual: {testStepStatus.Actual}");
        }
    }
}
