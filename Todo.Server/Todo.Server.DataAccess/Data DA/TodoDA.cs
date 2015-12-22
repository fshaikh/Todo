using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;
using MongoDB.Driver;

namespace Server.DataAccess
{
    public class TodoDA : DataAccessBase<Todo>
    {
        #region Constants
        private const string TODO_COLLECTION_NAME = "Todo";
        #endregion Constants

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="TodoDA"> />
        /// </summary>
        /// <param name="connectionInfo"></param>
        public TodoDA(ConnectionInfo connectionInfo)
            : base(connectionInfo)
        {
        }
        #endregion Constructors
        #region Data Access Methods

        #region Save Methods
        /// <summary>
        /// Saves the Todo item.
        /// </summary>
        /// <param name="item">Todo item to save</param>
        public async override Task<bool> Save(Todo item)
        {
            if (item.IsNew)
            {
                // insert the item
                var result = await Insert(item);
                return result;
            }
            if (item.IsChanged)
            {
                // item is updated. Update the item
                var result  = await Update(item);
                return result;
            }
            if (item.IsDeleted)
            {
                // delete the item
                var result = await Remove(item);
                return result;
            }
            return true;

        }


        /// <summary>
        /// This method inserts the Todo in the collection
        /// </summary>
        /// <param name="item"></param>
        private async Task<bool> Insert(Todo item)
        {
             var collection = Database.GetCollection<Todo>(TODO_COLLECTION_NAME);
             if (collection != null)
             {
                 await collection.InsertOneAsync(item);
                 return true;
             }
             return false;
        }

        /// <summary>
        /// Updates a Todo item based on the filter.
        /// </summary>
        /// <param name="item">Todo item which has updated state</param>
        private async Task<bool> Update(Todo item)
        {
            var collection = Database.GetCollection<Todo>(TODO_COLLECTION_NAME);
            if (collection != null)
            {
                var filter = Builders<Todo>.Filter.Eq("Id",item.Id.ToString());
                var update  = Builders<Todo>.Update
                                            .Set("Title",item.Title)
                                            .Set("Description",item.Description)
                                            .Set("Category",item.Category)
                                            .Set("State",item.State)
                                            .Set("LastModifiedTime",item.LastModifiedTime)
                                            .Set("IsCompleted",item.IsCompleted);
                var updateResult = await collection.UpdateOneAsync(filter, update);
                
                return updateResult.IsAcknowledged;
            }
            return false;
        }

        /// <summary>
        /// Removes the Todo item from the database. This is a hard delete.
        /// </summary>
        /// <param name="item">Item to delete</param>
        private async Task<bool> Remove(Todo item)
        {
            var collection = Database.GetCollection<Todo>(TODO_COLLECTION_NAME);
            if (collection != null)
            {
                var filter = Builders<Todo>.Filter.Eq("Id",item.Id.ToString());
                var deleteResult = await collection.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged;
            }
            return false;
        }

        #endregion Save Methods

        #region Load Methods

        /// <summary>
        /// Loads a specific Todo item based on the item id.
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns></returns>
        public override Task<Todo> Get(object id)
        {
            return base.Get(id);
        }

        /// <summary>
        /// Loads all todo items from the persistent storage
        /// </summary>
        /// <returns>Collection of items</returns>
        public override BaseCollection<Todo> GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// Loads all the Todos for a particular user. Uses Owner property
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<List<Todo>> GetTodosForUser(Guid userId)
        {
            var builder = Builders<Todo>.Filter;
            var filter = builder.Eq("Owner",userId.ToString());
           var result =  await GetTodosCore(userId, filter);
           return result;
        }

        /// <summary>
        /// Loads all the Todos for a particular user and Todo state. Uses Owner property
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="todoState"></param>
        /// <returns></returns>
        public virtual async Task<List<Todo>> GetTodosByState(Guid userId, TodoState todoState)
        {
            var builder = Builders<Todo>.Filter;
            var filter = builder.Eq("Owner", userId.ToString()) & builder.Eq("State", todoState);
            
            return await GetTodosCore(userId, filter);
        }

        /// <summary>
        /// Helper method to execute find queries against MongoDb
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="filterDefinition"></param>
        /// <returns></returns>
        private async Task<List<Todo>> GetTodosCore(Guid userId, FilterDefinition<Todo> filterDefinition)
        {
            List<Todo> todoCollection = null;
            var collection = Database.GetCollection<Todo>(TODO_COLLECTION_NAME);
            if (collection != null)
            {
                var sortDefinition = Builders<Todo>.Sort.Descending("CreatedTime");
                todoCollection = await collection.Find<Todo>(filterDefinition).Sort(sortDefinition).ToListAsync();
            }
            return todoCollection;
        }
        #endregion Load Methods

        #region Support Methods
        #endregion Support Methods
        #endregion Methods
    }
}
