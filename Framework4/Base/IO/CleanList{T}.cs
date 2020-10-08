using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Tiny.Framework.IO
{
    /// <summary>
    /// Clean list, a clean xml list...
    /// </summary>
    /// <typeparam name="T">the collection type.</typeparam>
    [CollectionDataContract]
    public class CleanList<T> : List<T>
    {
    }
}
