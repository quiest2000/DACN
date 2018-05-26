using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;

namespace HReception.Logic.Utils.Extensions
{
    public static class AutoMapperExtension
    {
        /// <summary>
        /// The map to.
        /// </summary>
        /// <param name="self">
        /// The self.
        /// </param>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static List<TResult> MapTo<TResult>(this IEnumerable self)
        {
            if (self == null)
            {
                return null;
            }

            return (List<TResult>)Mapper.Map(self, self.GetType(), typeof(List<TResult>));
        }

        /// <summary>
        /// The map to.
        /// </summary>
        /// <param name="self">
        /// The self.
        /// </param>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        /// The <see cref="TResult"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static TResult MapTo<TResult>(this object self) where TResult : class
        {
            if (self == null)
            {
                return null;
            }

            return (TResult)Mapper.Map(self, self.GetType(), typeof(TResult));
        }
        public static TResult MapTo<TResult>(this object self, TResult dest) where TResult : class
        {
            if (self == null)
            {
                return null;
            }

            return (TResult)Mapper.Map(self, dest, self.GetType(), typeof(TResult));
        }
    }
}

