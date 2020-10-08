// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
namespace Tiny.Framework.Web.ApplicationInsights.Configuration
{
    /// <summary>
    /// i configure monitoring.
    /// </summary>
    public interface IConfigureMonitoring
    {
        /// <summary>
        /// gets the insrumentation key.
        /// </summary>
        string InstrumentationKey { get; }

        /// <summary>
        /// gets the environment.
        /// </summary>
        string Environment { get; }

        /// <summary>
        /// gets the name of the hosted component.
        /// </summary>
        string Component { get; }
    }
}