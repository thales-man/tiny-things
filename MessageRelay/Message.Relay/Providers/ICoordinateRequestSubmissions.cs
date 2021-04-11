using MessageRelay.Models;

namespace MessageRelay.Providers
{
    /// <summary>
    /// I coordinate request submissions.
    /// </summary>
    public interface ICoordinateRequestSubmissions
    {
        /// <summary>
        /// Requires action.
        /// </summary>
        /// <returns><c>true</c>, if action was required, <c>false</c> otherwise.</returns>
        /// <param name="thisRequest">This request.</param>
        bool RequiresAction(IRelayRequest thisRequest);
    }
}
