namespace Tiny.Framework.Registration.Attributes
{
    /// <summary>
    /// the type of registration scope.
    /// </summary>
    public enum TypeOfRegistrationScope
    {
        /// <summary>
        /// Singleton.
        /// </summary>
        Singleton,

        /// <summary>
        /// Scoped.
        /// </summary>
        Scoped,

        /// <summary>
        /// Transient.
        /// </summary>
        Transient,
    }
}
