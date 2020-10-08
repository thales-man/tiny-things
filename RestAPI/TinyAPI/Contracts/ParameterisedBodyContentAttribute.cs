//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System;
using Tiny.Framework.Utility;

namespace Tiny.API.Contracts
{
    /// <summary>
    /// Parameterised body content attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public sealed class ParameterisedBodyContentAttribute : Attribute
    {
        /// <summary>
        /// to create.
        /// </summary>
        /// <value>To create.</value>
        public Type ToCreate { get; }

        /// <summary>
        /// to inject.
        /// </summary>
        /// <value>To inject.</value>
        public Type ToInject { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Tiny.API.Contracts.ParameterisedBodyContentAttribute"/> class.
        /// </summary>
        /// <param name="concreteType">Concrete type.</param>
        /// <param name="contractType">Contract type.</param>
        public ParameterisedBodyContentAttribute(Type concreteType, Type contractType)
        {
            concreteType.IsAssignableFrom(contractType)
                .AsGuard<ArgumentException>(nameof(contractType));

            ToCreate = concreteType;
            ToInject = contractType;
        }
    }
}
