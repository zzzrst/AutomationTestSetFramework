// <copyright file="ITestStepLogger.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace AutomationTestSetFramework
{
    /// <summary>
    /// Interface that represents the test step logger. It writes information about the test step.
    /// </summary>
    public interface ITestStepLogger
    {
        /// <summary>
        /// Writes to log information anout the test step.
        /// </summary>
        /// <param name="testStep">The test step to log.</param>
        public void Log(ITestStep testStep);
    }
}