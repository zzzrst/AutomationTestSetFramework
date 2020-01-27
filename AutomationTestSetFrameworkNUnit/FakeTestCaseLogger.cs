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
            Console.WriteLine(Tab(1) + $"Test Set Name: {testSet.Name}.");
            Console.WriteLine(Tab(1) + $"Test Set Curr Test Case Number: {testSet.CurrTestCaseNumber}.");
            Console.WriteLine(Tab(1) + $"Test Set Status");
            Console.WriteLine(Tab(1) + $"Run Successful: {testSetStatus.RunSuccessful}");
            Console.WriteLine(Tab(1) + $"Error Stack: {testSetStatus.ErrorStack}");
            Console.WriteLine(Tab(1) + $"Friendly Error Message: {testSetStatus.FriendlyErrorMessage}");
            Console.WriteLine(Tab(1) + $"Start Time: {testSetStatus.StartTime}");
            Console.WriteLine(Tab(1) + $"End Time: {testSetStatus.EndTime}");
            Console.WriteLine(Tab(1) + $"Description: {testSetStatus.Description}");
            Console.WriteLine(Tab(1) + $"Expected: {testSetStatus.Expected}");
            Console.WriteLine(Tab(1) + $"Actual: {testSetStatus.Actual}");
        }
    }
}
