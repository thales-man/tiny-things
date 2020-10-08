namespace Tiny.Framework.Diagnostic
{
    /// <summary>
    /// I create disagnostic messages.
    /// </summary>
    public interface ICreateDiagnosticMessages
    {
        /// <summary>
        /// Create the specified message.
        /// </summary>
        /// <returns>the create.</returns>
        /// <param name="message">the message.</param>
        /// <param name="level">the diagnostic level.</param>
        /// <returns>a diagnostic message.</returns>
        IDiagnosticMessage Create(string message, DiagnosticLevel level = DiagnosticLevel.Trace);
    }
}
