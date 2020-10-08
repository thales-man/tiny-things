using System.Composition;
using System.Threading.Tasks;
using AppKit;

namespace Tiny.Framework.Services.Internal
{
    [Shared]
    [Export(typeof(IAccessClipboard))]
    internal sealed class ClipboardAccess :
        IAccessClipboard
    {
        /// <summary>
        /// this has to be the tiny framework xamarin adapter
        /// and not the framework service adapter, check the registrations
        /// </summary>
        [Import]
        public IDeviceAdapter Adapter { get; set; }

        /// <summary>
        /// get text from the clipboard
        /// </summary>
        /// <returns>the currently running task</returns>
        public async Task<string> GetText() =>
            await Task.FromResult(GetTextInternal());

        /// <summary>
        /// put the text on the clipboard
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task PutText(string text) =>
            await Task.Run(() => SetTextInternal(text));

        private readonly string[] pbTypes = { "NSStringPboardType" };

        internal string GetTextInternal()
        {
            var value = string.Empty;

            // the clipboard is only available on the main thread
            Adapter.Begin(() =>
            {
                var pasteboard = NSPasteboard.GeneralPasteboard;
                value = pasteboard.GetStringForType(pbTypes[0]);
            });

            return value;
        }

        internal void SetTextInternal(string data)
        {
            // the clipboard is only available on the main thread
            Adapter.Begin(() =>
            {
                var pasteboard = NSPasteboard.GeneralPasteboard;

                pasteboard.ClearContents();
                pasteboard.DeclareTypes(pbTypes, null);
                pasteboard.SetStringForType(data, pbTypes[0]);
            });
        }
    }
}