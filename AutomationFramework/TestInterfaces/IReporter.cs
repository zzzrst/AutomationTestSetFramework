// <copyright file="IReporter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace AutomationTestSetFramework
{
    /// <summary>
    /// Interface for the reporter class. Reports to the tester the results usualy after a test step.
    /// </summary>
    public interface IReporter
    {
        /// <summary>
        /// Sends the report to the tester.
        /// </summary>
        public void Report();

        /// <summary>
        /// Adds a status for a test set.
        /// </summary>
        /// <param name="testSetStatus">The test set to add a status for.</param>
        public void AddTestSetStatus(ITestSetStatus testSetStatus);

        /// <summary>
        /// Adds a status for a test case.
        /// </summary>
        /// <param name="testCaseStatus">The test case to add a status for.</param>
        public void AddTestCaseStatus(ITestCaseStatus testCaseStatus);

        /// <summary>
        /// Adds a status for a test step.
        /// </summary>
        /// <param name="testStepStatus">The test step to add a status for.</param>
        /// <param name="testCaseStatus">The test case to add the status to.</param>
        public void AddTestStepStatusToTestCase(ITestStepStatus testStepStatus, ITestCaseStatus testCaseStatus);
    }
}
