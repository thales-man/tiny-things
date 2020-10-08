//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Linq;
using System.Reflection;
using Tiny.Framework.Factories;
using Tiny.Framework.Utility;
using Xamarin.Forms;

namespace Tiny.Framework.Markup
{
    /// <summary>
    /// a compositional class to automatically wire up a button to model operations
    /// </summary>
    public sealed class RelayCommandBehavior :
        BehaviorBase<Button>
    {
        private int _conditionParameterCount = 0;

        /// <summary>
        /// Initialises this instance.
        /// </summary>
        public override void Initialise()
        {
            // base.Initialise(); <= the abstracts' call

            if (It.Has(AssociatedObject.BindingContext))
            {
                LoadCommand();
            }
            else
            {
                AssociatedObject.BindingContextChanged += OnBindingContextChanged;
            }
        }

        /// <summary>
        /// Datas the context changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public void OnBindingContextChanged(object sender, EventArgs e)
        {
            AssociatedObject.BindingContextChanged -= OnBindingContextChanged;
            LoadCommand();
        }

        /// <summary>
        /// Loads the command.
        /// </summary>
        /// <exception cref="ArgumentNullException">method </exception>
        private void LoadCommand()
        {
            // need to consider setting the working context
            // maybe through the use of a converter so we don't mangle the behaviour

            // get the method
            var actionName = GetActionName();

            // create the command
            var action = BuildAction(actionName);
            var predicate = BuildPredicate(actionName);

            AssociatedObject.Command = CommandFactory.Create(action, predicate);
        }

        /// <summary>
        /// Builds the action.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <param name="method">The method.</param>
        /// <returns>an action</returns>
        private Action BuildAction(string actionName)
        {
            var model = AssociatedObject.BindingContext;
            var method = GetMethodInfo(model, actionName);

            It.IsNull(method)
                .AsGuard<ArgumentNullException>(nameof(actionName));

            IsMethodVoid(method);

            var _actionParameterCount = method.GetParameters().Count();

            (_actionParameterCount > 1)
                .AsGuard<ArgumentOutOfRangeException>(nameof(method));

            return _actionParameterCount == 1
              ? GetParameterisedAction(model, method)
              : GetAction(model, method);
        }

        /// <summary>
        /// Builds the predicate.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <param name="actionName">Name of the action</param>
        /// <returns>a func bool</returns>
        private Func<bool> BuildPredicate(string actionName)
        {
            // get the condition
            var conditionName = GetConditionName(actionName);
            var model = AssociatedObject.BindingContext;

            var condition = GetCondition(model, conditionName);

            It.IsNull(condition)
                .AsGuard<ArgumentNullException>(conditionName);

            (_conditionParameterCount > 1)
                .AsGuard<ArgumentOutOfRangeException>(conditionName);

            return (_conditionParameterCount == 1)
              ? GetParameterisedPredicate(model, condition)
              : GetPredicate(model, condition);
        }

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        /// <returns>the name of the action</returns>
        public string GetActionName()
        {
            var actionName = RelayCommandInjector.GetActionName(AssociatedObject);
            var actionPrefix = RelayCommandInjector.GetActionPrefix(AssociatedObject);

            return actionName.Contains(actionPrefix) ? actionName : $"{actionPrefix}{actionName}";
        }

        /// <summary>
        /// Gets the view model action
        /// currently there is no way to get the binding paramater types
        /// so we can only deal with parameterless commands
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="types">The types.</param>
        /// <returns>
        /// return an array of paramter types
        /// </returns>
        private static MethodInfo GetMethodInfo(object model, string methodName)
        {
            return model.GetType().GetTypeInfo().GetDeclaredMethod(methodName);
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="method">The method.</param>
        /// <returns>a delegate action</returns>
        private static Action GetAction(object model, MethodInfo method)
        {
            return delegate { method.Invoke(model, null); };
        }

        /// <summary>
        /// Gets the parameterised action.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="method">The method.</param>
        /// <returns>a parameterised delegate action</returns>
        private Action GetParameterisedAction(object model, MethodInfo method)
        {
            return delegate { method.Invoke(model, new object[] { AssociatedObject.CommandParameter }); };
        }

        /// <summary>
        /// Gets the name of the condition.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>the condition name</returns>
        public string GetConditionName(string methodName)
        {
            var conditionPrefix = RelayCommandInjector.GetConditionPrefix(AssociatedObject);
            return $"{conditionPrefix}{methodName}";
        }

        /// <summary>
        /// Gets the condition.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="conditionName">Name of the condition.</param>
        /// <param name="types">The types.</param>
        /// <returns>a propertyinfo object, a func<bool> or a default handler</returns>
        private object GetCondition(object model, string conditionName)
        {
            // do we have a predicate condition?
            object property = model.GetType().GetTypeInfo().GetDeclaredProperty(conditionName);

            // do we have a boolean method condition
            if (property == null)
            {
                var method = GetMethodInfo(model, conditionName);
                _conditionParameterCount = It.Has(method) ? method.GetParameters().Count() : 0;
                property = method;
            }

            // otherwise, it's an always on button..
            if (property == null)
            {
                property = new Func<bool>(() => { return true; });
            }

            return property;
        }

        /// <summary>
        /// Gets the predicate.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="condition">The condition.</param>
        /// <returns>converts the incoming item into a delegate for a func<bool></returns>
        private static Func<bool> GetPredicate(object model, object condition)
        {
            if (condition is Func<bool>)
            {
                return condition as Func<bool>;
            }

            if (condition is MethodInfo)
            {
                var method = condition as MethodInfo;

                IsFunctionBool(method);

                return delegate { return (bool)method.Invoke(model, null); };
            }

            return delegate { return (bool)(condition as PropertyInfo).GetValue(model, null); };
        }

        /// <summary>
        /// Gets the parameterised predicate.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="condition">The condition.</param>
        /// <returns>a func bool</returns>
        private Func<bool> GetParameterisedPredicate(object model, object condition)
        {
            if (condition is MethodInfo)
            {
                var method = condition as MethodInfo;

                IsFunctionBool(method);

                return delegate { return (bool)method.Invoke(model, new object[] { AssociatedObject.CommandParameter }); };
            }

            return delegate { return (bool)(condition as PropertyInfo).GetValue(model, null); };
        }

        /// <summary>
        /// Determines whether [is method void] [the specified method].
        /// </summary>
        /// <param name="method">The method.</param>
        /// <exception cref="ArgumentNullException">an action must be 'method void'</exception>
        private static void IsMethodVoid(MethodInfo method)
        {
            if (!typeof(void).GetTypeInfo().IsAssignableFrom(method.ReturnType.GetTypeInfo()))
            {
                throw new ArgumentNullException(method.Name, "an action must be 'method void'");
            }
        }

        /// <summary>
        /// Determines whether [is function bool] [the specified method].
        /// </summary>
        /// <param name="method">The method.</param>
        /// <exception cref="ArgumentNullException">a conditional test should not return void</exception>
        private static void IsFunctionBool(MethodInfo method)
        {
            if (!typeof(void).GetTypeInfo().IsAssignableFrom(method.ReturnType.GetTypeInfo()))
            {
                throw new ArgumentNullException(method.Name, "a conditional test should not return void");
            }
        }
    }
}