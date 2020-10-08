using System;
using System.Reflection;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Registration.Attributes
{
    /// <summary>
    /// the routine registration attribute.
    /// abstract for registering add / use routines, traditional static aspnet core start up routines.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class RoutineRegistrationAttribute : Attribute
    {
        /// <summary>
        /// is not static.
        /// </summary>
        /// <param name="thisType">this type.</param>
        /// <returns>true if this type is not static.</returns>
        internal bool IsNotStatic(Type thisType) =>
            !(thisType is { IsAbstract: true, IsSealed: true });

        /// <summary>
        /// get the method.
        /// </summary>
        /// <param name="thisType">this type.</param>
        /// <param name="methodName">the method name.</param>
        /// <returns>the instantiated method.</returns>
        protected MethodInfo GetMethod(Type thisType, string methodName)
        {
            var method = thisType.GetMethod(methodName);

            It.IsNull(method)
                .AsGuard<InvalidOperationException>($"Method '{methodName}' not found on '{thisType.Name}'");

            return method;
        }

        /// <summary>
        /// create action.
        /// </summary>
        /// <typeparam name="TAction">the type of action.</typeparam>
        /// <param name="method">the method.</param>
        /// <returns>the delegated action.</returns>
        protected TAction CreateAction<TAction>(MethodInfo method)
            where TAction : Delegate
        {
            var action = (TAction)method.CreateDelegate(typeof(TAction));

            It.IsNull(action)
                .AsGuard<ArgumentNullException>(nameof(action));

            return action;
        }
    }
}
