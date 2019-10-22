// <copyright file="IMethodBoundaryAspect.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationTestSetFramework
{
    using System;

    /// <summary>
    /// Interfaces that specifies the methods called from Fody's method boundary aspect.
    /// </summary>
    public interface IMethodBoundaryAspect
    {
        /// <summary>
        /// Called when an exception is thrown.
        /// </summary>
        /// <param name="e">The exception that is thrown by the method.</param>
        public void HandleException(Exception e);

        public FlowBehavior OnExceptionFlowBehavior { get; set; }

        /// <summary>
        /// Called right before the method is started to be executed.
        /// </summary>
        public void SetUp();

        /// <summary>
        /// Called right before the method is about to exit. Note, this method is called regardless if there was an exception / not.
        /// </summary>
        public void TearDown();

        public enum FlowBehavior
        {
            /// <summary>
            /// Continues the method.
            /// </summary>
            Continue,

            /// <summary>
            /// On default, it returns and calls on exit.
            /// </summary>
            Return,

            /// <summary>
            /// Rethrows the exception.
            /// </summary>
            RethrowException,
        }
    }
}
