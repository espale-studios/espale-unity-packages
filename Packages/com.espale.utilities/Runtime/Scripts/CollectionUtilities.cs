using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Espale.Utilities
{
    public static class CollectionUtilities
    {
        /// <summary>
        /// Returns an array containing the items of the given enumerable without the duplicates.
        /// </summary>
        /// <param name="enumerable">Enumerable to clear duplicates from</param>
        /// <returns>An array containing items of the given enumerable without the duplicates</returns>
        public static T[] RemoveDuplicatesFrom<T>(IEnumerable<T> enumerable)
        {
            var tempList = new List<T>();
            foreach (var item in enumerable)
            {
                if (!tempList.Contains(item))
                    tempList.Add(item);
            }

            return tempList.ToArray();
        }
        
        /// <summary>
        /// returns a random item from the given enumerable.
        /// </summary>
        /// <param name="enumerable">Enumerable to pick an item from</param>
        /// <returns>Randomly picked item from the given enumerable</returns>
        public static T GetRandomItemFrom<T>(IEnumerable<T> enumerable)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();
            return array[Random.Range(0, array.Length)];
        }
    }
}
