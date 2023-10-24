using AutomationTestSetFramework;
using static AutomationTestSetFramework.ITestStatus;
using System;


namespace AutomationTestSetFrameworkNUnit
{
    class FakeTestCaseStatus : ITestCaseStatus
    {
        public int TestCaseNumber { get; set; }
        public bool RunSuccessful { get; set; }
        public string ErrorStack { get; set; }
        public string FriendlyErrorMessage { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Expected { get; set; }
        public string Actual { get; set; }
        public string Name { get ; set ; }
    }
}
