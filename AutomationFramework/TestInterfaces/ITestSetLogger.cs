// <copyright file="ITestSet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace AutomationTestSetFramework
{
    /// <summary>
    /// Interface that represents the test set logger. It writes information about the test set.
    /// </summary>
    public interface ITestSetLogger
    {
        /// <summary>
        /// Writes to log information anout the test set.
        /// </summary>
        /// <param name="testSet">The test set to log.</param>
        public void Log(ITestSet testSet);
    }
}