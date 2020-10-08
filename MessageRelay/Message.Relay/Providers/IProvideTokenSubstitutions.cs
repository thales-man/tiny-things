//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

namespace MessageRelay.Providers
{
    /// <summary>
    /// I provide token substitutes.
    /// </summary>
    public interface IProvideTokenSubstitutions
    {
        /// <summary>
        /// Applies the substitutes to.
        /// </summary>
        /// <returns>The substitutes to.</returns>
        /// <param name="theCommand">The command.</param>
        string ApplySubstitutesTo(string theCommand);

        /// <summary>
        /// Gets the substitute for.
        /// </summary>
        /// <returns>The substitute value.</returns>
        /// <param name="theToken">The token.</param>
        string GetSubstituteFor(string theToken);
    }
}
