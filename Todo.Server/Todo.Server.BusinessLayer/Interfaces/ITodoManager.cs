using DataObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.BusinessLayer
{
    /// <summary>
    /// This interface defines methods for Todo management
    /// </summary>
    public interface ITodoManager
    {
        /// <summary>
        /// Saves a Todo. Based on the change state flags, create/update/delete will be executed
        /// </summary>
        /// <param name="todo">Todo item to be saved</param>
        /// <returns>Response</returns>
        Task<ResponseBase> Save(Todo todo);

        /// <summary>
        /// Gets all the Todos for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<GetItemsResponse<Todo>> GetAll(string userId);
    }
}
