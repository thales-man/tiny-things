namespace Tiny.Framework.Registration
{
    /// <summary>
    /// I support service registration (contract).
    /// Place this against every fault / exception that requires registration.
    /// </summary>
    public interface ISupportFaultRegistration :
        ISupportGenericHostRegistration
    {
    }
}
