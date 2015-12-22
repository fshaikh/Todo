using DataObjects;
using Server.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Web.Common;

namespace WebApi.Controllers
{
    /// <summary>
    /// Api controller for Todo
    /// </summary>
    [RoutePrefix("api")]
    public class TodoApiController : ApiControllerBase
    {
        #region Members
        private ITodoManager _todoManager;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Default constructor. This will be invoked by IoC container when it fails to resolve dependencies for this controller.
        /// </summary>
        public TodoApiController():base()
        {
            _todoManager = new TodoManager(ServerContext);
        }

        /// <summary>
        /// Constructor which declares the dependency. IoC container will call this when it has successfully resolved dependency and injected.
        /// </summary>
        /// <param name="todoManager"></param>
        public TodoApiController(ITodoManager todoManager)
        {
            _todoManager = todoManager;
        }
        #endregion Constructors

        #region Api Methods
        /// <summary>
        /// Api method to add todo
        /// </summary>
        /// <param name="todoDTO"></param>
        /// <returns></returns>
        [Route("todos"),HttpPost]
        public async Task<HttpResponseMessage> AddTodo(TodoDTO todoDTO)
        {
            // Get the Todo domain object from the DTO
            Todo todo = GetTodoObject(todoDTO);
            // Save the Todo using the manager
            var response = await _todoManager.Save(todo);

            return HandlePostResponse(todo,todoDTO,response);
        }

        /// <summary>
        /// Api method to get all todos for a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("todos/{userId}"), HttpGet]
        public async Task<HttpResponseMessage> Get(string userId)
        {
            var response = await _todoManager.GetAll(userId);

            return HandleGetTodosResponse(userId, response);
        }

        /// <summary>
        /// Api method to update the todo
        /// </summary>
        /// <param name="todoDTO"></param>
        /// <returns></returns>
        [Route("todos"),HttpPut]
        public async Task<HttpResponseMessage> UpdateTodo(TodoDTO todoDTO)
        {
            // Get the Todo domain object from the DTO
            Todo todo = GetTodoObject(todoDTO,true);
            
            // Save the Tdodo using manager
            var response = await _todoManager.Save(todo);

            return HandlePostResponse(todo, todoDTO, response);
        }

        // mark todo as complete
        // todos/{todo_id}  put
        // put todos/1
        /// <summary>
        /// Api method for a delete of todo
        /// </summary>
        /// <param name="todoId"></param>
        /// <returns></returns>
        [Route("todos/{todoId}"),HttpDelete]
        public async Task<HttpResponseMessage> Delete(string todoId)
        {
            Todo todo = new Todo(new Guid(todoId));
            todo.IsNew = false;
            todo.IsChanged = false;
            todo.IsDeleted = true;

            var response = await _todoManager.Save(todo);
            if (response.Success)
            {
                return CreateDeleteSuccessMessage<Todo>(todo);
            }
            else
            {
                // TODO: Handle error response
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to delete todo");
            }
        }

        
        #endregion Api Methods

        #region Support Methods
        /// <summary>
        /// This method transforms a Todo DTO to Todo domain object
        /// </summary>
        /// <param name="todoDTO"></param>
        /// <returns></returns>
        private Todo GetTodoObject(TodoDTO todoDTO,bool isupdate = false)
        {
            // TODO: Use AutoMapper
            Todo todo = new Todo();
            todo.Category = todoDTO.Category;
            // TODO: todo.CreatedByUserId = 
            todo.CreatedTime = DateTime.UtcNow;
            todo.Description = todoDTO.Description;
            todo.IsCompleted = todoDTO.IsCompleted;
            //TODO: todo.LastModifiedByUserId = 
            todo.LastModifiedTime = DateTime.UtcNow;
            todo.Owner = todoDTO.Owner;
            todo.State = todoDTO.State;
            todo.Title = todoDTO.Title;

            if (isupdate)
            {
                todo.Id = new Guid(todoDTO.Id);
                // set the change state flags
                todo.IsNew = false;
                todo.IsDeleted = false;
                todo.IsChanged = true;
            }

            todoDTO.Id = todo.Id.ToString();

            return todo;
        }

        /// <summary>
        /// This method handles the post response to be sent to user agent
        /// </summary>
        /// <param name="response">Response from business layer.Contains failures, if any and status</param>
        /// <param name="user">Todo domain object</param>
        /// <returns>HttpResponseMessage</returns>
        private HttpResponseMessage HandlePostResponse(Todo todo,TodoDTO todoDTO, ResponseBase response)
        {
            if (response.Success)
            {
                // TODO: Set location url for the new todo
                return base.CreatePostSuccessMessage<TodoDTO>("/todos/", todoDTO);
            }
            else
            {
                // TODO: Create an error response based on the failures
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to save Todo");
            }
        }

        /// <summary>
        /// Creates HTTP Response for Get All calls
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="response"></param>
        /// <param name="todos"></param>
        /// <returns></returns>
        private HttpResponseMessage HandleGetTodosResponse(string userId, GetItemsResponse<Todo> response)
        {
            if (response.Success)
            {
                // TODO: See if this can be defined in base class to handle all get all responses
                // TODO: Add hyperlinks based on hypermedia concept
                HttpResponseMessage message = Request.CreateResponse<List<Todo>>(HttpStatusCode.OK, response.Items);
                return message;
            }
            else
            {
                // TODO:Based on failures
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to load todos for this user");
            }
        }
        #endregion Support Methods
    }
}