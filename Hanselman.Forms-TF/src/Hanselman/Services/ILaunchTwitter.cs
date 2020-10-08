namespace Hanselman.Services
{
    /// <summary>
    /// I Launch twitter contract definition.
    /// </summary>
    public interface ILaunchTwitter
    {
        /// <summary>
        /// Opens the account for.
        /// </summary>
        /// <returns><c>true</c>, if account for was opened, <c>false</c> otherwise.</returns>
        /// <param name="thisUser">This user.</param>
        bool OpenAccountFor(string thisUser);

        /// <summary>
        /// Opens the item for.
        /// </summary>
        /// <returns><c>true</c>, if item for was opened, <c>false</c> otherwise.</returns>
        /// <param name="thisID">This identifier.</param>
        bool OpenItemFor(string thisID);
    }
}
