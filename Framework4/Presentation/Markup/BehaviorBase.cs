//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Tiny.Framework.Utility;
using Xamarin.Forms;

namespace Tiny.Framework.Markup
{
    /// <summary>
    /// a behavior base that handles loading and unloading of an interactive behavior
    /// 
    /// this caters for runtime loading and unloadng of controls placed in containers 
    /// such as expanders and tab controls as they are added and removed from the 
    /// visual tree
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BehaviorBase<T> :
        Behavior<T>
        where T : BindableObject
    {
        public T AssociatedObject
        {
            get
            {
                return BindingContext as T;
            }
        }

        /// <summary>
        /// Called when [attached].
        /// there is no detachment occuring in this layer as these latches are not 
        /// removed, hence their weak reference requirements
        /// </summary>
        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);

            if (It.Has(AssociatedObject))
            {
                Initialise();
            }
        }

        /// <summary>
        /// Initialises this instance.
        /// </summary>
        public abstract void Initialise();
    }
}