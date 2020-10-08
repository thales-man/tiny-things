namespace Tiny.Framework.Registration
{
    /// <summary>
    /// I support configuration registration (contract).
    /// Place this against every configuration that requires registration.
    /// </summary>
    public interface ISupportConfigurationRegistration :
        ISupportGenericHostRegistration
    {
    }
}
