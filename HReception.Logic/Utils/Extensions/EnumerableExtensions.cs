using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
namespace HReception.Logic.Utils.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> src)
        {
            return src == null || !src.Any();
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            Contract.Requires(range != null);
            Contract.Requires(collection != null);

            foreach (T curItem in range)
                collection.Add(curItem);
        }
    }
}