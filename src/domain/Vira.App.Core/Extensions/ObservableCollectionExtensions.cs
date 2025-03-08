using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vira.App.Core.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            // это не правильно , потому что очень много колбеков вызовется 
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}