namespace Tiny.Framework.Registration
{
    /// <summary>
    /// I support service registration (contract).
    /// Place this against every service that requires registration.
    /// </summary>
    public interface ISupportServiceRegistration :
        ISupportGenericHostRegistration
    {
    }
}
