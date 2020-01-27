// <copyright file="ITestCaseStatus.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationTestSetFramework
{
    /// <summary>
    /// Interface that represents the test case status object.
    /// </summary>
    public interface ITestCaseStatus : ITestStatus
    {
        /// <summary>
        /// Gets or sets the number of the test case this status is referring to.
        /// </summary>
        public int TestCaseNumber { get; set; }
    }
}