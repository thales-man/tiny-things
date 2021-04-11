using System;
using System.Composition;
using System.Windows.Input;
using Tiny.Framework.Diagnostic;
using Tiny.Framework.Flow;
using Tiny.Framework.Scaffolding;
using Tiny.Framework.Services;
using Xamarin.Forms;

namespace MessageRelay.ViewModels
{
    /// <summary>
    /// the console component, a log listener
    /// </summary>
    /// <seealso cref="ObservableItem" />
    /// <seealso cref="ISupportLogging " />
    /// <seealso cref="IMainViewModel " />
    [Shared]
    [Export(typeof(ISupportLogging))]
    [Export(typeof(IMainViewModel))]
    internal sealed class MainViewModel :
        ObservableItem,
        ISupportLogging,
        IMainViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            ClearConsoleCommand = new Command(() => ConsoleText = string.Empty);
            CopyConsoleCommand = new Command(() => Clipboard.PutText(ConsoleText));
        }

        /// <summary>
        /// Gets or sets the race condition manager
        /// </summary>
        [Import]
        public ISynchroniseActions Synchronise { get; set; }

        /// <summary>
        /// Gets or Sets the clipboard service
        /// </summary>
        [Import]
        public IAccessClipboard Clipboard { get; set; }

        /// <summary>
        /// Gets or sets the clear console command.
        /// </summary>
        public ICommand ClearConsoleCommand { get; set; }

        /// <summary>
        /// Gets or sets the copy console command.
        /// </summary>
        public ICommand CopyConsoleCommand { get; set; }

        /// <summary>
        /// Gets or sets the (page) title.
        /// </summary>
        public string Title => "Message Relay";

        /// <summary>
        /// The text
        /// </summary>
        private string _text = string.Empty;

        /// <summary>
        /// Gets or sets the console text.
        /// </summary>
        public string ConsoleText
        {
            get { return _text; }
            set { SetPropertyValue(ref _text, value); }
        }

        /// <summary>
        /// Adds the log entry.
        /// </summary>
        /// <param name="newEntry">New entry.</param>
        public void AddLogEntry(IDiagnosticPayload newEntry)
        {
            // if the console text is roughly bigger than 50k
            if (ConsoleText.Length > 50000)
            {
                ConsoleText = string.Empty;
            }

            Synchronise.Update(() => ConsoleText += Format(newEntry.Message));
        }

        /// <summary>
        /// Format the specified theLine.
        /// </summary>
        /// <returns>The format.</returns>
        /// <param name="theLine">The line.</param>
        public string Format(string theLine) =>
            $"[{DateTime.Now:ddd dd-MMM @hh:mm:ss tt}] {Cleanse(theLine)}\n";

        /// <summary>
        /// Cleanse the String.
        /// </summary>
        /// <returns>The cleansed.</returns>
        /// <param name="theString">The string.</param>
        public string Cleanse(string theString)
        {
            return theString
                .Replace("\r", " ")
                .Replace(Environment.NewLine, " ")
                .Replace("  ", " ")
                .Replace("\t", " ");
        }
    }
}
