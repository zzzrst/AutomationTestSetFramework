// <copyright file="AutomationTestSetDriver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationTestSetFramework
{
    using System;

    /// <summary>
    /// Main driver for the automation test set framework.
    /// </summary>
    public class AutomationTestSetDriver
    {
        /// <summary>
        /// Gets or sets the test set for the driver to use.
        /// </summary>
        public ITestSet TestSet { get; set; }

        /// <summary>
        /// Main method to start running the test set provided.
        /// Before running, it will run the test set's implementation of setup().
        /// If there is an exception during the run, it will call on the test set's implementation of handleException().
        /// Before exiting, it will then run the test set's implementation of teardown().
        /// </summary>
        /// <param name="testSet">The test set to be run.</param>
        [TestMethodBoundaryAspect]
        public void RunTestSet()
        {
            // We continue to run our test set if
            //                                    1. The next test case exists.
            //                                    2. We are supposed to execute.
            while (this.TestSet.ExistNextTestCase() && this.TestSet.ShouldExecute())
            {
                ITestCase testCase = this.TestSet.GetNextTestCase();

                // We call on Run Test Case to run the test case.
                // The test case status should be updated during the run.
                RunTestCase(testCase);

                // Update my test set status based on the test case status.
                this.TestSet.UpdateTestSetStatus(testCase.TestCaseStatus);
            }
        }

        [TestMethodBoundaryAspect]
        private static void RunTestCase(ITestCase testCase)
        {
            while (testCase.ExistNextTestStep() && testCase.ShouldExecute())
            {
                ITestStep testStep = testCase.GetNextTestStep();
                RunTestStep(testStep);
                testCase.UpdateTestCaseStatus(testStep.TestStepStatus);
            }
        }

        [TestMethodBoundaryAspect]
        private static void RunTestStep(ITestStep testStep)
        {
            while (testStep.ShouldExecute())
            {
                testStep.Execute();
            }
        }
    }
}
