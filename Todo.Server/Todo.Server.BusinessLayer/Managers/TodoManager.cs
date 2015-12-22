using DataObjects;
using Server.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer
{
    /// <summary>
    /// Manager implementation for Todos
    /// </summary>
    public class TodoManager : EditableManagerBase<Todo>, ITodoManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="TodoManager"/>
        /// </summary>
        /// <param name="serverContext">Server context</param>
        public TodoManager(IServerContext serverContext)
            : base(serverContext)
        {
            TodoDataAccess = new TodoDA(serverContext.ConnectionInfo);
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the data access object for Todo
        /// </summary>
        public TodoDA TodoDataAccess
        {
            get;
            private set;
        }
        #endregion Properties

        #region Methods

        #region ITodoManager Methods
        /// <summary>
        /// Saves a Todo. Based on the change state flags, create/update/delete will be executed
        /// </summary>
        /// <param name="todo">Todo item to be saved</param>
        /// <returns>Response</returns>
        public async Task<ResponseBase> Save(Todo todo)
        {
            // TODO: Handle exception. For eg: throw if user is empty

            ResponseBase responseBase = null;
            // Validate Todo 
            // If error , return model validation error

            // perform pre save operations,if any
            PerformPreSaveOperations(todo);

            // Save the todo using the data access object
            var result = await TodoDataAccess.Save(todo);
            if (!result)
            {
                // failed to save
                responseBase = CreateSaveErrorResponse(todo);
            }

            // perform post save operations, if any
            PerformPostSaveOperations();

            return new ResponseBase();
        }

        


        /// <summary>
        /// Gets all the Todos for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<GetItemsResponse<Todo>> GetAll(string userId)
        {
            // Throw if userid is null

            // TODO: Do validation
            // userId is in the right format, if not return failure
            // if userId is not the same as logged in one, return failure

            var result = await TodoDataAccess.GetTodosForUser(new Guid(userId));

            GetItemsResponse<Todo> response = new GetItemsResponse<Todo>(result);
            return response;
        }

        #endregion ITodoManagerMethods

        #region Override Methods

        protected override void PerformPreSaveOperations(Todo dataObject)
        {
            base.PerformPreSaveOperations(dataObject);
        }

        protected override void PerformPostSaveOperations()
        {

        }
        #endregion Override Methods

        #region Support Methods
        /// <summary>
        /// Creates an error response for save operation
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        private ResponseBase CreateSaveErrorResponse(Todo todo)
        {
            SaveType saveType = SaveType.Insert;
            if(todo.IsNew)
            {
                saveType = SaveType.Insert;
            }
            else if(todo.IsChanged)
            {
                saveType = SaveType.Update;
            }
            else if(todo.IsDeleted)
            {
                saveType = SaveType.Delete;
            }

            DataObjectSaveFailure<Todo> failure = new DataObjectSaveFailure<Todo>(todo, saveType);

            ResponseBase responseBase = new ResponseBase();
            responseBase.Failures.Add(failure);

            return responseBase;
        }
        #endregion Support Methods

        #endregion Methods
    }
}
