//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

namespace Http.Service.Contract.Headers
{

    /// <summary>
    /// i http response header
    /// </summary>
    public interface IHttpResponseHeader
    {

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        string Value { get; }
    }

    /// <summary>
    /// i http response header {T}
    /// </summary>
    /// <typeparam name="T">the header type</typeparam>
    public interface IHttpResponseHeader<T>: IHttpResponseHeader
    {

        /// <summary>
        /// Composes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        void Compose(T value);
    }
}
