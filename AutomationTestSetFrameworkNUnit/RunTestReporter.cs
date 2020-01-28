using AutomationTestSetFramework;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTestSetFrameworkNUnit
{
    class RunTestReporter
    {
        private ITestStepStatus TestStepStatus;
        private ITestCaseStatus TestCaseStatus;
        private ITestCaseStatus TestCaseStatusTwo;
        private ITestSetStatus TestSetStatus;
        private IReporter Reporter;

        [SetUp]
        public void Setup()
        {
            TestStepStatus = new FakeTestStepStatus()
            {
                RunSuccessful = false,
                ErrorStack = string.Empty,
                FriendlyErrorMessage = string.Empty,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Description = "Fake Test Step",
                Expected = string.Empty,
                Actual = string.Empty,
                TestStepNumber = 1
            };
            TestCaseStatus = new FakeTestCaseStatus()
            {
                RunSuccessful = false,
                ErrorStack = string.Empty,
                FriendlyErrorMessage = string.Empty,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Description = "Fake Test Case",
                Expected = string.Empty,
                Actual = string.Empty,
                TestCaseNumber = 1
            };
            TestCaseStatusTwo = new FakeTestCaseStatus()
            {
                RunSuccessful = false,
                ErrorStack = string.Empty,
                FriendlyErrorMessage = string.Empty,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Description = "Fake Test Case",
                Expected = string.Empty,
                Actual = string.Empty,
                TestCaseNumber = 1
            };
            TestSetStatus = new FakeTestSetStatus()
            {
                RunSuccessful = false,
                ErrorStack = string.Empty,
                FriendlyErrorMessage = string.Empty,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Description = "Fake Test Set",
                Expected = string.Empty,
                Actual = string.Empty,
            };
            Reporter = new FakeReporter()
            {
                TestSetStatuses = new List<ITestSetStatus>(),
                TestCaseStatuses = new List<ITestCaseStatus>(),
                TestCaseToTestSteps = new Dictionary<ITestCaseStatus, List<ITestStepStatus>>(),
            };
        }

        [Test]
        public void TestReportEmpty()
        {
            if (Reporter is FakeReporter fakeReporter)
            {
                fakeReporter.Report();
            }
        }

        [Test]
        public void TestReportAddOneSet()
        {
            FakeReporter fakeReporter = Reporter as FakeReporter;
            FakeTestSetStatus testSetStatus = TestSetStatus as FakeTestSetStatus;

            fakeReporter.AddTestSetStatus(testSetStatus);
            fakeReporter.Report();

            Assert.AreEqual(1, fakeReporter.TestSetStatuses.Count, "Expected the size of the testSetStatus to be 1");
        }

        [Test]
        public void TestReportAddOneCase()
        {
            FakeReporter fakeReporter = Reporter as FakeReporter;
            FakeTestCaseStatus testCaseStatus = TestCaseStatus as FakeTestCaseStatus;

            Reporter.AddTestCaseStatus(testCaseStatus);
            Reporter.Report();

            Assert.AreEqual(1, fakeReporter.TestCaseStatuses.Count, "Expected the size of the testCaseStatus to be 1");
        }

        [Test]
        public void TestReportAddOneStep()
        {
            FakeReporter fakeReporter = Reporter as FakeReporter;
            FakeTestCaseStatus testCaseStatus = TestCaseStatus as FakeTestCaseStatus;
            FakeTestStepStatus testStepStatus = TestStepStatus as FakeTestStepStatus;

            Reporter.AddTestCaseStatus(testCaseStatus);
            Reporter.AddTestStepStatusToTestCase(testStepStatus, testCaseStatus);
            Reporter.Report();

            Assert.AreEqual(1, fakeReporter.TestCaseStatuses.Count, "Expected the size of the testCaseStatus to be 1");
            Assert.AreEqual(1, fakeReporter.TestCaseToTestSteps.Count, "Expected the size of the TestCaseToTestSteps to be 1");
            Assert.AreEqual(1, fakeReporter.TestCaseToTestSteps[testCaseStatus].Count, "Expected the size of the list for testCaseStatus key to be 1");
        }

        [Test]
        public void TestReportAddTwoStep()
        {
            FakeReporter fakeReporter = Reporter as FakeReporter;
            FakeTestCaseStatus testCaseStatus = TestCaseStatus as FakeTestCaseStatus;
            FakeTestStepStatus testStepStatus = TestStepStatus as FakeTestStepStatus;
            FakeTestStepStatus testStepStatusTwo = TestStepStatus as FakeTestStepStatus;

            Reporter.AddTestCaseStatus(testCaseStatus);
            Reporter.AddTestStepStatusToTestCase(testStepStatus, testCaseStatus);
            Reporter.AddTestStepStatusToTestCase(testStepStatusTwo, testCaseStatus);
            Reporter.Report();

            Assert.AreEqual(1, fakeReporter.TestCaseStatuses.Count, "Expected the size of the testCaseStatus to be 1");
            Assert.AreEqual(1, fakeReporter.TestCaseToTestSteps.Count, "Expected the size of the TestCaseToTestSteps to be 1");
            Assert.AreEqual(2, fakeReporter.TestCaseToTestSteps[testCaseStatus].Count, "Expected the size of the list for testCaseStatus key to be 2");
        }

        [Test]
        public void TestReportAddTwoStepsDifferentCase()
        {
            FakeReporter fakeReporter = Reporter as FakeReporter;
            FakeTestCaseStatus testCaseStatus = TestCaseStatus as FakeTestCaseStatus;
            FakeTestCaseStatus testCaseStatusTwo = TestCaseStatusTwo as FakeTestCaseStatus;
            FakeTestStepStatus testStepStatus = TestStepStatus as FakeTestStepStatus;
            FakeTestStepStatus testStepStatusTwo = TestStepStatus as FakeTestStepStatus;

            Reporter.AddTestCaseStatus(testCaseStatus);
            Reporter.AddTestCaseStatus(testCaseStatusTwo);
            Reporter.AddTestStepStatusToTestCase(testStepStatus, testCaseStatus);
            Reporter.AddTestStepStatusToTestCase(testStepStatusTwo, testCaseStatusTwo);
            Reporter.Report();

            Assert.AreEqual(2, fakeReporter.TestCaseStatuses.Count, "Expected the size of the testCaseStatus to be 2");
            Assert.AreEqual(2, fakeReporter.TestCaseToTestSteps.Count, "Expected the size of the TestCaseToTestSteps to be 2");
            Assert.AreEqual(1, fakeReporter.TestCaseToTestSteps[testCaseStatus].Count, "Expected the size of the list for testCaseStatus key to be 1");
            Assert.AreEqual(1, fakeReporter.TestCaseToTestSteps[testCaseStatusTwo].Count, "Expected the size of the list for testCaseStatusTwo key to be 1");
        }
    }
}
