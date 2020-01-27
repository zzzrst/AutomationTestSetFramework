// <copyright file="ITestCaseLogger.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace AutomationTestSetFramework
{
    /// <summary>
    /// Interface that represents the test case logger. It writes information about the test case.
    /// </summary>
    public interface ITestCaseLogger
    {
        /// <summary>
        /// Writes to log information anout the test case.
        /// </summary>
        /// <param name="testCase">The test case to log.</param>
        public void Log(ITestCase testCase);
    }
}