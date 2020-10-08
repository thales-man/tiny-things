namespace Tiny.Framework.Registration
{
   /// <summary>
    /// I support routine registration (contract).
    /// Place this against every extension routine that requires registration.
    /// </summary>
    public interface ISupportRoutineRegistration :
        ISupportGenericHostRegistration
    {
    }
}
