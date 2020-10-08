//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Flow.Internal
{
    /// <summary>
    /// A weak action reference (base).
    /// </summary>
    internal abstract class WeakActionReferenceBase
    {
        /// <summary>
        /// the weak reference.
        /// </summary>
        private readonly WeakReference _weakRef;

        /// <summary>
        /// the owner type.
        /// </summary>
        private readonly Type _ownerType;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakActionReferenceBase" /> class.
        /// </summary>
        /// <param name="type">the type.</param>
        /// <param name="target">the target.</param>
        protected WeakActionReferenceBase(Type type, object target)
        {
            _ownerType = type;
            _weakRef = new WeakReference(target);
        }

        /// <summary>
        /// gets a value indicating whether this instance has been collected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has been collected; otherwise, <c>false</c>.
        /// </value>
        public bool HasBeenCollected
        {
            get { return _ownerType == null && (_weakRef == null || !_weakRef.IsAlive); }
        }

        /// <summary>
        /// gets a value indicating whether this instance is alive.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is invocable; otherwise, <c>false</c>.
        /// </value>
        public bool IsAlive
        {
            get { return !HasBeenCollected; }
        }
    }
}
