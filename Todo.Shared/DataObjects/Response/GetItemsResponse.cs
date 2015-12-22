using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Response for Get items
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetItemsResponse<T> : ResponseBase where T:DataObjectBase
    {
        #region Constructors
        public GetItemsResponse(List<T> items)
        {
            Items = items;
        }
        #endregion Constructors
        #region Properties
        public List<T> Items { get; private set; }
        #endregion Properties
    }
}
