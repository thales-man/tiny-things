using System;
using System.Composition;
using MessageRelay.Models;
using Tiny.Framework.Flow;
using Tiny.Framework.Providers;

namespace MessageRelay.Providers.Internal
{
    [Shared]
    [Export(typeof(ICoordinateRequestSubmissions))]
    public class RequestSubmissionCoordinator :
        MappedContentProvider<Guid, IRelayRequest>,
        ICoordinateRequestSubmissions
    {
        const int ShortLapse = 50000000; // 5 seconds in ticks

        /// <summary>
        /// Gets or sets the synchronise (reset event).
        /// </summary>
        /// <value>The synchronise.</value>
        [Import]
        public ISynchroniseActions Synchronise { get; set; }

        /// <summary>
        /// Fetchs the default.
        /// </summary>
        /// <returns>The default.</returns>
        /// <param name="theKey">Key.</param>
        public override IRelayRequest FetchDefault(Guid theKey)
        {
            return null;
        }

        /// <summary>
        /// Requires action.
        /// </summary>
        /// <returns><c>true</c>, if action is required, <c>false</c> otherwise.</returns>
        /// <param name="thisRequest">This request.</param>
        public bool RequiresAction(IRelayRequest thisRequest)
        {
            Synchronise.WaitTillReady();
            Synchronise.Start();

            if (Holds(thisRequest.Device))
            {
                var previous = Fetch(thisRequest.Device);
                Update(thisRequest.Device, thisRequest);
                Synchronise.Finish();

                return thisRequest.Trigger != previous.Trigger;
            }

            Add(thisRequest.Device, thisRequest);
            Synchronise.Finish();
            return true;
        }
    }
}
