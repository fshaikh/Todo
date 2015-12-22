using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace Server.DataAccess
{
    /// <summary>
    /// Interface for all data access classes
    /// </summary>
    /// <typeparam name="T">Object that derives from DataObjectBase</typeparam>
    public interface IDataAccess<T> where T: DataObjectBase
    {
        /// <summary>
        /// Saves the item.
        /// </summary>
        /// <param name="item"></param>
        Task<bool> Save(T item);

        /// <summary>
        /// Loads a specific item based on the item id.
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns></returns>
        Task<T> Get(object id);

        /// <summary>
        /// Loads all items from the persistent storage
        /// </summary>
        /// <returns>Collection of items</returns>
        BaseCollection<T> GetAll();
    }
}
