namespace Tiny.Framework.Data.Factories
{
    /// <summary>
    /// i create data access scopes.
    /// </summary>
    /// <typeparam name="TContractScope">teh data access scope.</typeparam>
    public interface ICreateAccessScopes<out TContractScope>
        where TContractScope : class, IScopeAccessToData
    {
        /// <summary>
        /// begin scope.
        /// </summary>
        /// <returns>a new data access scope.</returns>
        TContractScope BeginScope();
    }
}
