// <copyright file="ITestSet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace AutomationTestSetFramework
{
    /// <summary>
    /// Interface that represents a test set object. It contains all the neccessary information to run a test set.
    /// </summary>
    public interface ITestSet : IMethodBoundaryAspect
    {
        /// <summary>
        /// Gets or sets the name of the test set.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the total number of test cases.
        /// </summary>
        public int TotalTestCases { get; set; }

        /// <summary>
        /// Gets the current test set status.
        /// </summary>
        public ITestSetStatus TestSetStatus { get; }

        /// <summary>
        /// Gets or Sets the current test case number.
        /// </summary>
        public int CurrTestCaseNumber { get; set; }

        /// <summary>
        /// Returns whether the next test case exist or not.
        /// </summary>
        /// <returns>True if there is a next test case.</returns>
        public bool ExistNextTestCase();

        /// <summary>
        /// Returns whther the next test case should be executed or not.
        /// </summary>
        /// <returns>True if the next test case should be executed. </returns>
        public bool ShouldExecute();

        /// <summary>
        /// Gets the next test case.
        /// </summary>
        /// <returns>The next test case.</returns>
        public ITestCase GetNextTestCase();

        /// <summary>
        /// Updates the current test set status.
        /// </summary>
        /// <param name="testCaseStatus">The Test Case Status.</param>
        public void UpdateTestSetStatus(ITestCaseStatus testCaseStatus);
    }
}
