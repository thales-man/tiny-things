//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Composition;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Tiny.Framework.Registration;

namespace Tiny.Framework.IO.Internal
{
    /// <summary>
    /// a third party to first party referencing class.
    /// </summary>
    /// <seealso cref="ISerializeJsonTypes" />
    [Shared]
    [Export(typeof(ISerializeTypes))]
    [Export(typeof(ISerializeJsonTypes))]
    internal class JsonConverter :
        ISerializeJsonTypes,
        ISupportServiceRegistration
    {
        /// <summary>
        /// the settings.
        /// </summary>
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings();

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonConverter"/> class.
        /// </summary>
        public JsonConverter()
        {
            _settings.Formatting = Formatting.Indented;
            _settings.Converters.Add(new StringEnumConverter());
        }

        /// <summary>
        /// gets the type of the serialiser.
        /// </summary>
        public SerializerType SerialiserType
        {
            get
            {
                return SerializerType.JSON;
            }
        }

        /// <summary>
        /// Froms the string.
        /// </summary>
        /// <param name="item">the item.</param>
        /// <param name="classType">Type of the class.</param>
        /// <returns>
        /// a re-hydrated class of T.
        /// </returns>
        public object FromString(string item, Type classType)
        {
            return JsonConvert.DeserializeObject(item, classType, _settings);
        }

        /// <summary>
        /// From string.
        /// </summary>
        /// <typeparam name="T">the type to return.</typeparam>
        /// <param name="item">the item.</param>
        /// <returns>
        /// a re-hydrated class of T.
        /// </returns>
        public T FromString<T>(string item)
            where T : class
        {
            return JsonConvert.DeserializeObject<T>(item, _settings);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <typeparam name="T">the type to return.</typeparam>
        /// <param name="item">the item.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public string ToString<T>(T item)
            where T : class
        {
            return JsonConvert.SerializeObject(item, _settings);
        }

        /// <summary>
        /// Tos the string.
        /// </summary>
        /// <returns>the string.</returns>
        /// <param name="item">Item.</param>
        /// <param name="itemType">Item type.</param>
        public string ToString(object item, Type itemType)
        {
            return JsonConvert.SerializeObject(item, itemType, _settings);
        }
    }
}
