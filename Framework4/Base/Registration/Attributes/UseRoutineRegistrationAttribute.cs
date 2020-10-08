using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Registration.Attributes
{
    /// <summary>
    /// the use (service) routine registration attribute.
    /// for registering 'use' routines, traditional static aspnet core start up routines.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class UseRoutineRegistrationAttribute :
        RoutineRegistrationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UseRoutineRegistrationAttribute"/> class.
        /// </summary>
        /// <param name="staticType">the static type.</param>
        /// <param name="functionName">the function name.</param>
        public UseRoutineRegistrationAttribute(Type staticType, string functionName)
        {
            IsNotStatic(staticType)
                .AsGuard<InvalidOperationException>($"'{staticType.Name}' is not static");

            var method = GetMethod(staticType, functionName);
            UseAction = CreateAction<ActionType>(method);
        }

        /// <summary>
        /// the action type.
        /// </summary>
        /// <param name="builder">the application builder.</param>
        /// <param name="conf">the configuration provider.</param>
        /// <param name="env">the environement provider.</param>
        public delegate void ActionType(IApplicationBuilder builder, IConfiguration conf, IHostEnvironment env);

        /// <summary>
        /// gets the use action.
        /// </summary>
        public ActionType UseAction { get; }
    }
}
