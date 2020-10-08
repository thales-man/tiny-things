using System;
using System.Runtime.Serialization;

namespace Tiny.Framework.Faults
{
    /// <summary>
    /// i service response fault.
    /// </summary>
    public interface IServiceResponseFault :
        ISerializable
    {
        /// <summary>
        /// gets the fault exception type.
        /// </summary>
        Type ExceptionType { get; }

        /// <summary>
        /// gets the fault exception name.
        /// </summary>
        string ExceptionName { get; }
    }
}