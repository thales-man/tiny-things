//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Tiny.Framework.Utility
{
    /// <summary>
    /// removes the need to 'to list' prior to 'for eaching'
    /// implements a null patterned to safe list.
    /// </summary>
    public static class Collection
    {
        /// <summary>
        /// To safe list, null pattern safety.
        /// </summary>
        /// <typeparam name="T">of type.</typeparam>
        /// <returns>an empty collection of <typeparamref name="T"/>.</returns>
        public static ICollection<T> Empty<T>()
        {
            return new List<T>();
        }

        /// <summary>
        /// Empties the and read only.
        /// </summary>
        /// <typeparam name="T">of type.</typeparam>
        /// <returns>
        /// an empty readonly safe collection.
        /// </returns>
        public static IReadOnlyCollection<T> EmptyAndReadOnly<T>()
        {
            return new List<T>().SafeReadOnlyList();
        }

        /// <summary>
        /// As a safe list, null pattern safety.
        /// </summary>
        /// <typeparam name="T">of type.</typeparam>
        /// <param name="list">the list.</param>
        /// <returns>
        /// a safe collection.
        /// </returns>
        public static ICollection<T> AsSafeList<T>(this IEnumerable<T> list)
        {
            return list.SafeList();
        }

        /// <summary>
        /// As a safe readonly list.
        /// </summary>
        /// <typeparam name="T">of type.</typeparam>
        /// <param name="list">the list.</param>
        /// <returns>
        /// a readonly safe collection.
        /// </returns>
        public static IReadOnlyCollection<T> AsSafeReadOnlyList<T>(this IEnumerable<T> list)
        {
            return list.SafeReadOnlyList();
        }

        /// <summary>
        /// Safe any.
        /// </summary>
        /// <typeparam name="T">of type.</typeparam>
        /// <param name="list">the list.</param>
        /// <param name="expression">the expression.</param>
        /// <returns>
        /// the result of the expression.
        /// </returns>
        public static bool SafeAny<T>(this IEnumerable<T> list, Func<T, bool> expression)
        {
            var safeList = list.SafeReadOnlyList();
            return safeList.Any(expression);
        }

        /// <summary>
        /// Safes the where.
        /// </summary>
        /// <typeparam name="T">of type.</typeparam>
        /// <param name="list">the list.</param>
        /// <param name="expression">the expression.</param>
        /// <returns>
        /// the result of the expression.
        /// </returns>
        public static IEnumerable<T> SafeWhere<T>(this IEnumerable<T> list, Func<T, bool> expression)
        {
            var safeList = list.SafeReadOnlyList();
            return safeList.Where(expression);
        }

        /// <summary>
        /// For each, to safe list and conducts the action.
        /// </summary>
        /// <typeparam name="T">of type.</typeparam>
        /// <param name="collection">the collection.</param>
        /// <param name="action">the action.</param>
        public static void ForEach<T>(this ICollection<T> collection, Action<T> action)
        {
            It.IsNull(action)
                .AsGuard<ArgumentNullException>();

            var items = collection.AsSafeList();
            foreach (var item in items)
            {
                action(item);
            }
        }

        /// <summary>
        /// For each, to safe list and conducts the action.
        /// </summary>
        /// <typeparam name="T">of type.</typeparam>
        /// <param name="collection">the collection.</param>
        /// <param name="action">the action.</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            It.IsNull(action)
                .AsGuard<ArgumentNullException>();

            var items = collection.AsSafeList();
            foreach (var item in items)
            {
                action(item);
            }
        }

        /// <summary>
        /// Safe list, the private implemntation of null coalescing.
        /// </summary>
        /// <typeparam name="T">of type.</typeparam>
        /// <param name="list">the list.</param>
        /// <returns>
        /// a safe list.
        /// </returns>
        private static List<T> SafeList<T>(this IEnumerable<T> list)
        {
            return (list ?? new List<T>()).ToList();
        }

        /// <summary>
        /// Safes the read only list.
        /// </summary>
        /// <typeparam name="T">of type.</typeparam>
        /// <param name="list">the list.</param>
        /// <returns>
        /// a safe readonly list.
        /// </returns>
        private static ReadOnlyCollection<T> SafeReadOnlyList<T>(this IEnumerable<T> list)
        {
            return new ReadOnlyCollection<T>(list.SafeList());
        }
    }
}
