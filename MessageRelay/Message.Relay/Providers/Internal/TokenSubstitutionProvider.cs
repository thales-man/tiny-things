using System.Composition;
using System.Linq;
using MessageRelay.Models;
using Tiny.Framework;
using Tiny.Framework.IO;
using Tiny.Framework.Utility;

namespace MessageRelay.Providers.Internal
{
    /// <summary>
    /// Apple script provider.
    /// </summary>
    [Shared]
    [Export(typeof(IProvideTokenSubstitutions))]
    internal sealed class TokenSubstitutionProvider :
        IProvideTokenSubstitutions
    {
        /// <summary>
        /// Gets or sets the asset (manager).
        /// </summary>
        [Import]
        public IManageAssets Assets { get; set; }

        /// <summary>
        /// Gets or sets the hydrater.
        /// </summary>
        [Import]
        public ISerializeXMLTypes Hydrate { get; set; }

        /// <summary>
        /// Gets or sets the script store.
        /// </summary>
        public TokenSubstituteStore TokenStore { get; set; }

        /// <summary>
        /// Compose this instance.
        /// </summary>
        [OnImportsSatisfied]
        public void Compose()
        {
            var file = Assets.GetTextAsset(StoreName.ForTokenSubstitutions);
            TokenStore = Hydrate.FromString<TokenSubstituteStore>(file);
        }

        /// <summary>
        /// Applies the substitutes to.
        /// </summary>
        /// <returns>The substitutes to.</returns>
        /// <param name="theCommand">The command.</param>
        public string ApplySubstitutesTo(string theCommand)
        {
            var theCandidate = theCommand;

            TokenStore.TokenSubstitutes
                .ForEach(x => theCandidate = theCandidate.Replace(x.Token, x.Substitute));

            return theCandidate;
        }

        /// <summary>
        /// Gets the substitute for.
        /// </summary>
        /// <returns>The substitute for.</returns>
        /// <param name="theToken">The token.</param>
        public string GetSubstituteFor(string theToken)
        {
            var token = TokenStore.TokenSubstitutes
                .FirstOrDefault(x => x.Token.ComparesWith(theToken));

            return token?.Substitute;
        }
    }
}
