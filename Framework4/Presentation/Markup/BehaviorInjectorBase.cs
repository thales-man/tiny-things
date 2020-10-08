//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Xamarin.Forms;

namespace Tiny.Framework.Markup
{

    /// <summary>
    /// don't forget to implement:
    ///     public static bool GetAttached(DependencyObject obj) {
    ///     return (bool)obj.GetValue(AttachedProperty);
    ///     }
    /// </summary>
    /// <typeparam name="TControl">The type of the control.</typeparam>
    /// <typeparam name="TBehaviour">The type of the behaviour.</typeparam>
    public abstract class BehaviorInjectorBase<TControl, TBehaviour>
      where TControl : BindableObject
      where TBehaviour : BehaviorBase<TControl>, new()
    {
        public readonly static BindableProperty AttachedProperty = BindableProperty.CreateAttached(
            "Attached",
            typeof(bool),
            typeof(TBehaviour),
            false,
            propertyChanged: OnAttachedChanged);

        public static bool GetAttached(BindableObject obj)
        {
            return (bool)obj.GetValue(AttachedProperty);
        }

        public static void SetAttached(BindableObject obj, bool value)
        {
            obj.SetValue(AttachedProperty, value);
        }

        public readonly static BindableProperty AttachedBehaviourProperty = BindableProperty.CreateAttached(
          "AttachedBehaviour",
          typeof(TBehaviour),
          typeof(TBehaviour),
          null);

        protected static TBehaviour GetAttachedBehaviour(BindableObject obj)
        {
            return (TBehaviour)obj.GetValue(AttachedBehaviourProperty);
        }

        private static void SetAttachedBehaviour(BindableObject obj, TBehaviour behaviour)
        {
            obj.SetValue(AttachedBehaviourProperty, behaviour);
        }

        private static void AttachBehaviour(BindableObject d)
        {
            var behaviour = new TBehaviour
            {
                BindingContext = d
            };

            behaviour.Initialise();
            SetAttachedBehaviour(d, behaviour);
        }

        private static void DetachBehaviour(BindableObject d)
        {
            var behaviour = GetAttachedBehaviour(d);
            if (behaviour != null)
            {
                d.ClearValue(AttachedBehaviourProperty);
            }
        }

        private static void OnAttachedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((bool)newValue)
            {
                AttachBehaviour(bindable);
            }
            else
            {
                DetachBehaviour(bindable);
            }
        }
    }
}
