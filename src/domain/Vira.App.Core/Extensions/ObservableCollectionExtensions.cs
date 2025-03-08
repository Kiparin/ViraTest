using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vira.App.Core.Extensions
{
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// Добавить диапазон элементов в коллекцию
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}