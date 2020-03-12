// <copyright file="ITestStatus.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace AutomationTestSetFramework
{
    using System;

    /// <summary>
    /// Interface that represents status you would want to get from a test.
    /// </summary>
    public interface ITestStatus
    {
        /// <summary>
        /// Gets or sets a value indicating whether the Name of the Test Set/Case/Step.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the run was successful or not.
        /// </summary>
        public bool RunSuccessful { get; set; }

        /// <summary>
        /// Gets or sets the error stack trace if run was not successful.
        /// </summary>
        public string ErrorStack { get; set; }

        /// <summary>
        /// Gets or sets the friendly error message.
        /// </summary>
        public string FriendlyErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the start time of the test.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of the test.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the desciption for the test.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the expected for the test.
        /// </summary>
        public string Expected { get; set; }

        /// <summary>
        /// Gets or sets the actual for the test.
        /// </summary>
        public string Actual { get; set; }
    }
}