// <copyright file="ITestCase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationTestSetFramework
{
    /// <summary>
    /// The Test Case Interface to be consumed. It contains all the neccessary information to run a test case.
    /// </summary>
    public interface ITestCase : IMethodBoundaryAspect
    {
        /// <summary>
        /// Gets or sets the name of the test case.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the total number of test steps the test case contains.
        /// </summary>
        public int TotalTestSteps { get; set; }

        /// <summary>
        /// Gets the current status of the test case.
        /// </summary>
        public ITestCaseStatus TestCaseStatus { get; }

        /// <summary>
        /// Returns whether there is a next test step or not.
        /// </summary>
        /// <returns>True if there is a next test step.</returns>
        public bool ExistNextTestStep();

        /// <summary>
        /// Returns the next test step of type ITestStep.
        /// </summary>
        /// <returns>The next test step to be run.</returns>
        public ITestStep GetNextTestStep();

        /// <summary>
        /// Updates the current test case status based on the test step status.
        /// </summary>
        /// <param name="testStepStatus">The status of the test step.</param>
        public void UpdateTestCaseStatus(ITestStepStatus testStepStatus);

        /// <summary>
        /// Returns whether or not to execute the next test step.
        /// </summary>
        /// <returns>True if the next test step should be executed.</returns>
        public bool ShouldExecute();
    }
}
