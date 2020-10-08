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
    /// Parameter source attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ParameterMapAttribute : Attribute
    {
        /// <summary>
        /// from parameter.
        /// </summary>
        /// <value>From parameter.</value>
        public string FromParameter { get; }

        /// <summary>
        /// Gets to property.
        /// </summary>
        /// <value>To property.</value>
        public string ToProperty { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Tiny.API.Contracts.ParameterSourceAttribute"/> class.
        /// </summary>
        /// <param name="theParameter">The parameter.</param>
        public ParameterMapAttribute(string theParameter, string theProperty)
        {
            It.IsEmpty(theParameter)
                .AsGuard<ArgumentNullException>(nameof(theParameter));
            It.IsEmpty(theProperty)
                .AsGuard<ArgumentNullException>(nameof(theProperty));

            FromParameter = theParameter;
            ToProperty = theProperty;
        }
    }
}
