using System.Composition;
using System.Linq;
using MessageRelay.Models;
using Tiny.Framework;
using Tiny.Framework.IO;

namespace MessageRelay.Providers.Internal
{
    /// <summary>
    /// Apple script provider.
    /// </summary>
    [Shared]
    [Export(typeof(IProvideAppleScripts))]
    internal sealed class AppleScriptProvider :
        IProvideAppleScripts
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
        public AppleScriptStore ScriptStore { get; set; }

        /// <summary>
        /// Compose this instance.
        /// </summary>
        [OnImportsSatisfied]
        public void Compose()
        {
            var file = Assets.GetTextAsset(StoreName.ForAppleScripts);
            ScriptStore = Hydrate.FromString<AppleScriptStore>(file);
        }

        /// <summary>
        /// Gets the script for.
        /// </summary>
        /// <returns>The script for.</returns>
        /// <param name="theCommand">The command.</param>
        public string GetScriptFor(MessageCommand theCommand)
        {
            var selected = ScriptStore.AppleScripts
                .FirstOrDefault(x => x.Command == theCommand);

            return selected?.Script;
        }

        /// <summary>
        /// Gets the script for.
        /// </summary>
        /// <returns>The script for.</returns>
        /// <param name="theTrigger">The trigger.</param>
        public string GetScriptFor(GeoFenceTrigger theTrigger)
        {
            var selected = ScriptStore.AppleScripts
                .FirstOrDefault(x => x.Command == theTrigger.AsMessageCommand());

            return selected?.Script;
        }
    }
}
