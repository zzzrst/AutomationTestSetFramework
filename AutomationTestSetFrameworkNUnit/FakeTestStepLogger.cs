using AutomationTestSetFramework;
using System;

namespace AutomationTestSetFrameworkNUnit
{
    class FakeTestStepLogger : ITestStepLogger
    {
        public void Log(ITestStep testStep)
        {
            Console.WriteLine($"Test Step Name: {testStep.Name}.");
            Console.WriteLine($"Test Step Number: {testStep.TestStepNumber}.");
        }
    }
}
