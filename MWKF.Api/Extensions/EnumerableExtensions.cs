﻿using System;
using System.Collections.Generic;

namespace AUSKF.Api.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Ons the specified f.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <returns></returns>
        public static Func<T, T> On<T>(this Func<T, T> f, Func<T, T> g)
        {
            return t => g(f(t));
        }

        /// <summary>
        ///   Iterates through the specified enumerable object.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="enumerable"> The enumerable. </param>
        /// <param name="function"> The function. </param>
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> function)
        {
            foreach (var t in enumerable)
            {
                function(t);
            }
        }

        /// <summary>
        ///   Apply a given function to each element of a collection, returning a new collection with the items altered by function.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <typeparam name="TR"> The type of the new enumerable. </typeparam>
        /// <param name="enumerable"> The enumerable. </param>
        /// <param name="function"> The function. </param>
        /// <returns> </returns>
        public static IEnumerable<TR> Map<T, TR>(this IEnumerable<T> enumerable, Func<T, TR> function)
        {
            foreach (var t in enumerable)
            {
                yield return function(t);
            }
        }
    }
}