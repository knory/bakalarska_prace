using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class IListExtensions
    {
        /// <summary>
        /// Shuffles the elements in the list using the Fisher-Yates shuffle algorithm.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">List to be shuffled.</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            var random = new Random();

            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = random.Next(n + 1);
                T item = list[k];
                list[k] = list[n];
                list[n] = item;
            }
        }
    }
}
