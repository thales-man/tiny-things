using System;
using System.Composition;
using System.Diagnostics;
using Tiny.Framework.Utility;
using Xamarin.Essentials;

namespace Hanselman.Services.Internal
{
    [Shared]
    [Export(typeof(ILaunchTwitter))]
    internal sealed class LaunchTwitter :
        ILaunchTwitter
    {
        /// <summary>
        /// Opens the URL.
        /// </summary>
        /// <returns><c>true</c>, if URL was opened, <c>false</c> otherwise.</returns>
        /// <param name="thisURL">This URL.</param>
        public bool OpenURL(string thisURL)
        {
            Launcher.OpenAsync(thisURL);
            return true;
        }

        /// <summary>
        /// Reports the problem.
        /// </summary>
        /// <param name="thisProblem">This problem.</param>
        public void ReportProblem(Exception thisProblem) =>
            Debug.WriteLine($"Unable to launch url:\n\t{thisProblem.Message}");

        /// <summary>
        /// Opens the account for.
        /// </summary>
        /// <returns><c>true</c>, if account for was opened, <c>false</c> otherwise.</returns>
        /// <param name="thisUser">This user.</param>
        public bool OpenAccountFor(string thisUser)
        {
            var done = SafeActions.Try(
                () => OpenURL($"twitter://user?screen_name={thisUser}"),
                x => ReportProblem(x));

            if (!done)
            {
                done = SafeActions.Try(
                    () => OpenURL($"tweetbot://{thisUser}/timeline"),
                    x => ReportProblem(x));
            }

            return done;
        }

        /// <summary>
        /// Opens the item for.
        /// </summary>
        /// <returns><c>true</c>, if item for was opened, <c>false</c> otherwise.</returns>
        /// <param name="thisID">This identifier.</param>
        public bool OpenItemFor(string thisID)
        {
            var done = SafeActions.Try(
                () => OpenURL($"twitter://status?id={thisID}"),
                x => ReportProblem(x));

            if (!done)
            {
                done = SafeActions.Try(
                    () => OpenURL($"tweetbot:///status/{thisID}"),
                    x => ReportProblem(x));
            }

            return done;
        }
    }
}