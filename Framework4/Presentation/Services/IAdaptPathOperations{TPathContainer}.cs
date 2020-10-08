namespace Tiny.Framework.Services
{
    /// <summary>
    /// I adapt path operations.
    /// </summary>
    public interface IAdaptPathOperations<out TPathContainer> :
        IResolvePathOperations<TPathContainer>,
        IVisitPathGenerators
        where TPathContainer : class
    {
    }
}
