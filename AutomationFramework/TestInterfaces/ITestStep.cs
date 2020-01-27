// <copyright file="ITestStep.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace AutomationTestSetFramework
{
    /// <summary>
    /// Interface that represents a test set object. It contains all the neccessary information to run a test step.
    /// </summary>
    public interface ITestStep : IMethodBoundaryAspect
    {
        /// <summary>
        /// Gets or sets the name of the test step.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of the test step.
        /// </summary>
        public int TestStepNumber { get; set; }

        /// <summary>
        /// Gets or sets the current test step status.
        /// </summary>
        public ITestStepStatus TestStepStatus { get; set; }

        /// <summary>
        /// Execution of the test step.
        /// </summary>
        public void Execute();

        /// <summary>
        /// Indicates whether the test step needs to be run / not.
        /// </summary>
        /// <returns>True if we shuold execute the test step.</returns>
        public bool ShouldExecute();
    }
}
