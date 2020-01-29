// <copyright file="ITestStepStatus.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace AutomationTestSetFramework
{
    /// <summary>
    /// Interface that represents the test step status object.
    /// </summary>
    public interface ITestStepStatus : ITestStatus
    {
        /// <summary>
        /// Gets or sets the test step number that corresponds to the test step.
        /// </summary>
        public int TestStepNumber { get; set; }
    }
}