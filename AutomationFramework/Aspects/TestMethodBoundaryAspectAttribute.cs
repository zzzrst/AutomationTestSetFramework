// <copyright file="TestMethodBoundaryAspectAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationTestSetFramework
{
    using System;
    using MethodBoundaryAspect.Fody.Attributes;

    /// <summary>
    /// This is the method boundary aspect for test (test sets, test cases and test steps).
    /// Like any testing framework, you have a setup and teardown.
    /// We leverage OnMethodBoundaryAspect from Fody to wrap the run test methods.
    /// </summary>
    public class TestMethodBoundaryAspectAttribute : OnMethodBoundaryAspect
    {
        /// <summary>
        /// We override the OnEntry method to call on the setup method provided by the concrete class.
        /// </summary>
        /// <param name="args">The arguments of the method.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Method will have args.")]
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (args.Arguments[0] is IMethodBoundaryAspect firstArgument)
            {
                firstArgument.SetUp();
                base.OnEntry(args);
            }
            else
            {
                throw new ArgumentException($"{nameof(args)} {ResourceHelper.GetString("IMethodBoundaryAspectCastingError")}");
            }
        }

        /// <summary>
        /// We override the OnException method to call on the HandleException method provided by the concrete class.
        /// Afterwards, we call on the onExit method.
        /// </summary>
        /// <param name="args">The arguments of the method.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Method will have args.")]
        public override void OnException(MethodExecutionArgs args)
        {
            if (args.Arguments[0] is IMethodBoundaryAspect firstArgument)
            {
                firstArgument.HandleException(args.Exception);

                IMethodBoundaryAspect.FlowBehavior flowBehavior = firstArgument.OnExceptionFlowBehavior;

                switch (flowBehavior)
                {
                    case IMethodBoundaryAspect.FlowBehavior.Continue:
                        args.Method.Invoke(null, args.Arguments);
                        return;

                    case IMethodBoundaryAspect.FlowBehavior.RethrowException:
                        args.FlowBehavior = FlowBehavior.RethrowException;
                        return;

                    case IMethodBoundaryAspect.FlowBehavior.Return:
                        this.OnExit(args);
                        args.FlowBehavior = FlowBehavior.Return;
                        return;

                    default:
                        this.OnExit(args);
                        args.FlowBehavior = FlowBehavior.Return;
                        return;
                }
            }
            else
            {
                throw new ArgumentException($"{nameof(args)} {ResourceHelper.GetString("IMethodBoundaryAspectCastingError")}");
            }
        }

        /// <summary>
        /// We override the OnExit mtehod to call on the TearDown method provided by the concrete class.
        /// </summary>
        /// <param name="args">The arguments of the method.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Method will have args.")]
        public override void OnExit(MethodExecutionArgs args)
        {
            if (args.Arguments[0] is IMethodBoundaryAspect firstArgument)
            {
                firstArgument.TearDown();
                base.OnExit(args);
            }
            else
            {
                throw new ArgumentException($"{nameof(args)} {ResourceHelper.GetString("IMethodBoundaryAspectCastingError")}");
            }
        }
    }
}
