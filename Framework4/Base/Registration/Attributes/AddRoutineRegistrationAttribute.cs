using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Registration.Attributes
{
    /// <summary>
    /// the add (service) routine registration attribute.
    /// for registering 'add' routines, traditional static aspnet core start up routines.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class AddRoutineRegistrationAttribute :
        RoutineRegistrationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRoutineRegistrationAttribute"/> class.
        /// </summary>
        /// <param name="staticType">the static type.</param>
        /// <param name="functionName">the function name.</param>
        public AddRoutineRegistrationAttribute(Type staticType, string functionName)
        {
            IsNotStatic(staticType)
                .AsGuard<InvalidOperationException>($"'{staticType.Name}' is not static");

            var method = GetMethod(staticType, functionName);
            AddAction = CreateAction<ActionType>(method);
        }

        /// <summary>
        /// the action type.
        /// </summary>
        /// <param name="services">the services collection.</param>
        /// <param name="conf">the configuration provider.</param>
        /// <param name="env">the environement provider.</param>
        public delegate void ActionType(IServiceCollection services, IConfiguration conf, IHostEnvironment env);

        /// <summary>
        /// gets the build action.
        /// </summary>
        public ActionType AddAction { get; }
    }
}
