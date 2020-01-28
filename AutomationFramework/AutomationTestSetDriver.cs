// <copyright file="AutomationTestSetDriver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationTestSetFramework
{
    using System;

    /// <summary>
    /// Main driver for the automation test set framework.
    /// </summary>
    public static class AutomationTestSetDriver
    {
        /// <summary>
        /// Main method to start running the test set provided.
        /// Before running, it will run the test set's implementation of setup().
        /// If there is an exception during the run, it will call on the test set's implementation of handleException().
        /// Before exiting, it will then run the test set's implementation of teardown().
        /// </summary>
        /// <param name="testSet">The test set to be run.</param>
        [TestMethodBoundaryAspect]
        public static void RunTestSet(ITestSet testSet)
        {
            if (testSet == null)
            {
                throw new ArgumentNullException($"{ResourceHelper.GetString("TestSetNullExceptionMessage")}");
            }

            // We continue to run our test set if
            //                                    1. The next test case exists.
            //                                    2. We are supposed to execute.
            while (testSet.ShouldExecute() && testSet.ExistNextTestCase())
            {
                ITestCase testCase = testSet.GetNextTestCase();

                // We call on Run Test Case to run the test case.
                // The test case status should be updated during the run.
                RunTestCase(testCase);

                // Update my test set status based on the test case status.
                testSet.UpdateTestSetStatus(testCase.TestCaseStatus);
            }
        }

        /// <summary>
        /// Run the Test Case that is given.
        /// </summary>
        /// <param name="testCase">Test case that is provided.</param>
        [TestMethodBoundaryAspect]
        public static void RunTestCase(ITestCase testCase)
        {
            if (testCase == null)
            {
                throw new ArgumentNullException($"{ResourceHelper.GetString("TestCaseNullExceptionMessage")}");
            }

            while (testCase.ShouldExecute() && testCase.ExistNextTestStep())
            {
                ITestStep testStep = testCase.GetNextTestStep();
                RunTestStep(testStep);
                testCase.UpdateTestCaseStatus(testStep.TestStepStatus);
            }
        }

        /// <summary>
        /// Run the test step that is provided.
        /// </summary>
        /// <param name="testStep">The test Step that is provided.</param>
        [TestMethodBoundaryAspect]
        public static void RunTestStep(ITestStep testStep)
        {
            if (testStep == null)
            {
                throw new ArgumentNullException($"{ResourceHelper.GetString("TestStepNullExceptionMessage")}");
            }

            while (testStep.ShouldExecute())
            {
                testStep.Execute();
            }
        }
    }
}
