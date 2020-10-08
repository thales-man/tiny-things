//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Xamarin.Forms;

namespace Tiny.Framework.Markup
{
    /// <summary>
    /// an auto wiring button to command to action behaviour injector
    /// </summary>
    public sealed class RelayCommandInjector :
        BehaviorInjectorBase<Button, RelayCommandBehavior>
    {
        public static readonly BindableProperty WorkingContextProperty = BindableProperty.CreateAttached(
            "WorkingContext",
            typeof(object),
            typeof(RelayCommandInjector),
            null,
            propertyChanged: OnUpdateBehaviour);

        /// <summary>
        /// Gets the working context.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static object GetWorkingContext(BindableObject obj)
        {
            return obj.GetValue(WorkingContextProperty);
        }

        /// <summary>
        /// Sets the working context.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetWorkingContext(BindableObject obj, object value)
        {
            obj.SetValue(WorkingContextProperty, value);
        }

        /// <summary>
        /// The action name property
        /// </summary>
        public static readonly BindableProperty ActionNameProperty = BindableProperty.CreateAttached(
            "ActionName",
            typeof(string),
            typeof(RelayCommandInjector),
            null,
            propertyChanged: OnUpdateBehaviour);

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string GetActionName(BindableObject obj)
        {
            return (string)obj.GetValue(ActionNameProperty);
        }

        /// <summary>
        /// Sets the name of the action.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetActionName(BindableObject obj, string value)
        {
            obj.SetValue(ActionNameProperty, value);
        }

        /// <summary>
        /// The condition prefix property
        /// </summary>
        public static readonly BindableProperty ConditionPrefixProperty = BindableProperty.CreateAttached(
            "ConditionPrefix",
            typeof(string),
            typeof(RelayCommandInjector),
            "Can",
            propertyChanged: OnUpdateBehaviour);

        /// <summary>
        /// Gets the condition prefix.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string GetConditionPrefix(BindableObject obj)
        {
            return (string)obj.GetValue(ConditionPrefixProperty);
        }

        /// <summary>
        /// Sets the condition prefix.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetConditionPrefix(BindableObject obj, string value)
        {
            obj.SetValue(ConditionPrefixProperty, value);
        }

        /// <summary>
        /// The action prefix property
        /// </summary>
        public static readonly BindableProperty ActionPrefixProperty = BindableProperty.CreateAttached(
            "ActionPrefix",
            typeof(string),
            typeof(RelayCommandInjector),
            "Do",
            propertyChanged: OnUpdateBehaviour);

        /// <summary>
        /// Gets the action prefix.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string GetActionPrefix(BindableObject obj)
        {
            return (string)obj.GetValue(ActionPrefixProperty);
        }

        /// <summary>
        /// Sets the action prefix.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetActionPrefix(BindableObject obj, string value)
        {
            obj.SetValue(ActionPrefixProperty, value);
        }

        /// <summary>
        /// Called when [update behaviour].
        /// </summary>
        /// <param name="bindable">The b.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnUpdateBehaviour(BindableObject bindable, object oldValue, object newValue)
        {
            SetAttached(bindable, true);
        }
    }
}
