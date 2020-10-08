//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Composition;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Tiny.Framework.Registration;
using Tiny.Framework.Utility;

namespace Tiny.Framework.IO.Internal
{
    /// <summary>
    /// An xml data contract serializer.
    /// </summary>
    [Shared]
    [Export(typeof(ISerializeTypes))]
    [Export(typeof(ISerializeXMLTypes))]
    internal class XMLConverter :
        ISerializeXMLTypes,
        ISupportServiceRegistration
    {
        /// <summary>
        /// gets the type of the serialiser.
        /// </summary>
        public SerializerType SerialiserType
        {
            get
            {
                return SerializerType.XML;
            }
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <typeparam name="T">the type to be resolved.</typeparam>
        /// <param name="item">the item.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        string ISerializeTypes.ToString<T>(T item)
        {
            return ToString(item, typeof(T));
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="item">Item.</param>
        /// <param name="itemType">Item type.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        string ISerializeTypes.ToString(object item, Type itemType)
        {
            return ToString(item, itemType);
        }

        /// <summary>
        /// From string.
        /// </summary>
        /// <param name="item">the item.</param>
        /// <param name="classType">Type of the class.</param>
        /// <returns>
        /// a re-hydrated class of T.
        /// </returns>
        object ISerializeTypes.FromString(string item, Type classType)
        {
            return FromString(item, classType);
        }

        /// <summary>
        /// converts to type T from the string.
        /// requires UTF-8 encoded strings only.
        /// </summary>
        /// <typeparam name="T">the type to be resolved.</typeparam>
        /// <param name="item">the item.</param>
        /// <returns>
        /// a re-hydrated class of T.
        /// </returns>
        T ISerializeTypes.FromString<T>(string item)
        {
            return FromString<T>(item);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="item">the item.</param>
        /// <param name="itemType">Item type.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        private static string ToString(object item, Type itemType)
        {
            var decodedBytes = ToBytes(item, itemType);
            return Encoding.UTF8.GetString(decodedBytes, 0, decodedBytes.Length);
        }

        /// <summary>
        /// to bytes.
        /// </summary>
        /// <param name="item">the item.</param>
        /// <param name="type">of type.</param>
        /// <returns>a byte array.</returns>
        private static byte[] ToBytes(object item, Type type)
        {
            It.IsNull(item)
                .AsGuard<ArgumentNullException>();

            byte[] output;

            try
            {
                var stream = new MemoryStream();
                var settings = new XmlWriterSettings { Indent = true };
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    var serializer = new DataContractSerializer(type);
                    serializer.WriteObject(writer, item);
                }

                stream.Flush();
                output = stream.ToArray();
            }
            catch
            {
                throw new InvalidDataException(string.Format("the following type failed to serialise: {0}", type));
            }

            return output;
        }

        /// <summary>
        /// Froms the string.
        /// </summary>
        /// <param name="item">the item.</param>
        /// <param name="classType">Type of the class.</param>
        /// <returns>
        /// a re-hydrated class of T.
        /// </returns>
        private static object FromString(string item, Type classType)
        {
            It.IsNull(item)
                .AsGuard<ArgumentNullException>();

            return FromBytes(Encoding.UTF8.GetBytes(item), classType);
        }

        /// <summary>
        /// converts to type T from the string.
        /// requires UTF-8 encoded strings only.
        /// </summary>
        /// <typeparam name="T">the type to be resolved.</typeparam>
        /// <param name="item">the item.</param>
        /// <returns>
        /// a re-hydrated class of T.
        /// </returns>
        private static T FromString<T>(string item)
            where T : class
        {
            It.IsNull(item)
                .AsGuard<ArgumentNullException>();

            return (T)FromBytes(Encoding.UTF8.GetBytes(item), typeof(T));
        }

        /// <summary>
        /// Froms the bytes.
        /// </summary>
        /// <param name="item">the item.</param>
        /// <param name="classType">Type of the class.</param>
        /// <returns>
        /// a re-hydrated class of T.
        /// </returns>
        /// <exception cref="InvalidDataException">Can raise an exception.</exception>
        private static object FromBytes(byte[] item, Type classType)
        {
            It.IsNull(item)
                .AsGuard<ArgumentNullException>();

            object returnItem = null;

            using (var stream = new MemoryStream(item))
            {
                try
                {
                    using (var reader = XmlReader.Create(stream))
                    {
                        var serializer = new DataContractSerializer(classType);
                        returnItem = serializer.ReadObject(reader);
                    }
                }
                catch
                {
                    if (It.IsNull(returnItem))
                    {
                        throw new InvalidDataException(string.Format("the following type failed to deserialise: {0}", classType.Name));
                    }
                }
            }

            return returnItem;
        }
    }
}
