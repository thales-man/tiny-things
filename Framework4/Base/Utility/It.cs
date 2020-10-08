//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiny.Framework.Utility
{
    /// <summary>
    /// class encapsulating many type and content evaluation routines.
    /// </summary>
    public static class It
    {
        /// <summary>
        /// is value type.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="value">the value.</param>
        /// <returns>true or false.</returns>
        public static bool IsValueType<T>(object value)
            where T : IComparable
        {
            // FIX: not sure if 'is nested' is right
            return Has(value) && (value is T || typeof(T).IsNested);
        }

        /// <summary>
        /// is type.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="value">the value.</param>
        /// <returns>true or false.</returns>
        public static bool IsType<T>(object value)
            where T : class =>

            // FIX: not sure if 'is nested' is right
            Has(value) && (value is T || typeof(T).IsNested);

        /// <summary>
        /// is not type.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="value">the value.</param>
        /// <returns>true or false.</returns>
        public static bool IsNotType<T>(object value)
            where T : class, IComparable =>
            !IsType<T>(value);

        /// <summary>
        /// is null.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="value">the value.</param>
        /// <returns>true or false.</returns>
        public static bool IsNull<T>(T value)
            where T : class =>
            value == null;

        /// <summary>
        /// has.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="value">the value.</param>
        /// <returns>true or false.</returns>
        public static bool Has<T>(T value)
            where T : class =>
            value != null;

        /// <summary>
        /// has.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="value">the value.</param>
        /// <returns>true or false.</returns>
        public static bool Has<T>(T? value)
            where T : struct =>
            value != null;

        /// <summary>
        /// has.
        /// </summary>
        /// <param name="value">the value.</param>
        /// <returns>true or false.</returns>
        public static bool Has(string value) =>
            !string.IsNullOrWhiteSpace(value);

        /// <summary>
        /// has values.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="values">the values.</param>
        /// <returns>true or false.</returns>
        public static bool HasValues<T>(IEnumerable<T> values) =>
            Has(values) && values.Any();

        /// <summary>
        /// is empty.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="values">the values.</param>
        /// <returns>true or false.</returns>
        public static bool IsEmpty<T>(IEnumerable<T> values) =>
            IsNull(values) || !values.Any();

        /// <summary>
        /// is empty.
        /// </summary>
        /// <param name="value">the value.</param>
        /// <returns>true or false.</returns>
        public static bool IsEmpty(string value) =>
            string.IsNullOrWhiteSpace(value);

        /// <summary>
        /// is empty.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="value">the value.</param>
        /// <returns>true or false.</returns>
        public static bool IsEmpty<T>(T? value)
            where T : struct =>
            !value.HasValue;

        /// <summary>
        /// is empty.
        /// </summary>
        /// <param name="value">the value.</param>
        /// <returns>true or false.</returns>
        public static bool IsEmpty(Guid value) =>
            !IsUsable(value);

        /// <summary>
        /// is usable.
        /// </summary>
        /// <param name="value">the value.</param>
        /// <returns>true or false.</returns>
        public static bool IsUsable(Guid value) =>
            !value.Equals(Guid.Empty);

        /// <summary>
        /// is usable.
        /// </summary>
        /// <param name="value">the value.</param>
        /// <param name="parsedOut">the parsed out value.</param>
        /// <param name="min">the minimum.</param>
        /// <param name="max">the maximum.</param>
        /// <returns>true or false.</returns>
        public static bool IsUsable(string value, out int parsedOut, int min = int.MinValue, int max = int.MaxValue) =>
            int.TryParse(value, out parsedOut) && IsBetween(parsedOut, min, max);

        /// <summary>
        /// is in range.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="source">the source.</param>
        /// <param name="target">the target.</param>
        /// <returns>true or false.</returns>
        public static bool IsInRange<T>(T source, params T[] target)
            where T : IComparable
        {
            var values = target.AsSafeList();
            return values.Contains(source);
        }

        /// <summary>
        /// is in range.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="source">the source.</param>
        /// <param name="target">the target.</param>
        /// <returns>true or false.</returns>
        public static bool IsInRange<T>(T? source, params T[] target)
            where T : struct, IComparable, IFormattable
        {
            var values = target.AsSafeList();
            return Has(source) && values.Contains(source.Value);
        }

        /// <summary>
        /// is out of range.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="source">the source.</param>
        /// <param name="target">the target.</param>
        /// <returns>
        ///   <c>true</c> if [is out of range] [the specified source]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOutOfRange<T>(T source, params T[] target)
            where T : IComparable =>
            !IsInRange(source, target);

        /// <summary>
        /// is out of range.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="source">the source.</param>
        /// <param name="target">the target.</param>
        /// <returns>
        ///   <c>true</c> if [is out of range] [the specified source]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOutOfRange<T>(T? source, params T[] target)
            where T : struct, IComparable, IFormattable
        {
            var values = target.AsSafeList();
            return Has(source) && !values.Contains(source.Value);
        }

        /// <summary>
        /// is in range.
        /// </summary>
        /// <param name="candidate">the candidate.</param>
        /// <param name="min">the minimum.</param>
        /// <param name="max">the maximum.</param>
        /// <param name="includeBoundaries">if set to <c>true</c> [include boundaries].</param>
        /// <returns>true or false.</returns>
        public static bool IsBetween(int candidate, int min, int max, bool includeBoundaries = true) =>
            includeBoundaries
                ? candidate >= min && candidate <= max
                : candidate > min && candidate < max;

        /// <summary>
        /// is between.
        /// </summary>
        /// <param name="candidate">the candidate.</param>
        /// <param name="min">the minimum.</param>
        /// <param name="max">the maximum.</param>
        /// <param name="includeBoundaries">if set to <c>true</c> [include boundaries].</param>
        /// <returns>
        ///   <c>true</c> if the specified candidate is between; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBetween(TimeSpan candidate, TimeSpan min, TimeSpan max, bool includeBoundaries = true) =>
            includeBoundaries
                ? candidate >= min && candidate <= max
                : candidate > min && candidate < max;

        /// <summary>
        /// is between.
        /// </summary>
        /// <param name="candidate">the candidate.</param>
        /// <param name="min">the minimum.</param>
        /// <param name="max">the maximum.</param>
        /// <param name="includeBoundaries">if set to <c>true</c> [include boundaries].</param>
        /// <returns>
        ///   <c>true</c> if the specified candidate is between; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBetween(DateTime candidate, DateTime min, DateTime max, bool includeBoundaries = true) =>
            includeBoundaries
                ? candidate >= min && candidate <= max
                : candidate > min && candidate < max;

        /// <summary>
        /// is out of range.
        /// </summary>
        /// <param name="candidate">the candidate.</param>
        /// <param name="min">the minimum.</param>
        /// <param name="max">the maximum.</param>
        /// <param name="includeBoundaries">if set to <c>true</c> [include boundaries].</param>
        /// <returns>true or false.</returns>
        public static bool IsNotBetween(int candidate, int min, int max, bool includeBoundaries = true) =>
            !IsBetween(candidate, min, max, includeBoundaries);

        /// <summary>
        /// has count of.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="values">the values.</param>
        /// <param name="expectedCount">the expected count.</param>
        /// <returns>true or false.</returns>
        public static bool HasCountOf<T>(IEnumerable<T> values, int expectedCount)
        {
            var enumerable = values.AsSafeList();
            return enumerable.Count == expectedCount;
        }

        /// <summary>
        /// is the same.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">b.</param>
        /// <returns>true or false.</returns>
        public static bool IsTheSame(string a, string b) =>
            string.Compare(a, b, StringComparison.OrdinalIgnoreCase) == 0;

        /// <summary>
        /// is different.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">b.</param>
        /// <returns>true or false.</returns>
        public static bool IsDifferent(string a, string b) =>
            !IsTheSame(a, b);

        /// <summary>
        /// is different.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <param name="oldValue">the old value.</param>
        /// <param name="newValue">the new value.</param>
        /// <returns>
        ///   <c>true</c> if the specified old value is different; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDifferent<T>(T oldValue, T newValue) =>
            !((Equals(oldValue, default(T)) && Equals(newValue, default(T))) || Equals(oldValue, newValue));

        /// <summary>
        /// is defined.
        /// </summary>
        /// <typeparam name="TEnum">the type.</typeparam>
        /// <param name="item">the item.</param>
        /// <returns>true or false.</returns>
        public static bool IsDefined<TEnum>(string item)
            where TEnum : struct, IComparable, IFormattable =>
            Enum.TryParse(item, true, out TEnum parsed);

        /// <summary>
        /// is even.
        /// </summary>
        /// <param name="value">the value.</param>
        /// <returns>true or false.</returns>
        public static bool IsEven(int value) =>
            value % 2 == 0;

        /// <summary>
        /// is odd.
        /// </summary>
        /// <param name="value">the value.</param>
        /// <returns>true or false.</returns>
        public static bool IsOdd(int value) =>
            !IsEven(value);
    }
}
