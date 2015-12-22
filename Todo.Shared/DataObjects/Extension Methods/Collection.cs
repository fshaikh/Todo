using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public static class CollectionExtension
    {
        public static void AddIfNotNull<T>(this ICollection<T> collection,T item)
        {
            if (item != null)
                collection.Add(item);
        }
    }
}
