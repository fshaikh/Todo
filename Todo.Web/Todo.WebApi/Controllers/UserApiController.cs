using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;
using System.Web.Http;
using Web.Common;
using System.Net.Http;
using System.Net;
using Server.BusinessLayer;

namespace WebApi.Controllers
{
    /// <summary>
    /// API Controller for handling User resource
    /// </summary>
    [RoutePrefix("api")]
    public class UserController : ApiControllerBase
    {
        #region Members
        private ISecurityManager _securityManager;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public UserController():base()
        {
            _securityManager = new SecurityManager(ServerContext);
        }

        /// <summary>
        /// Initializes a new instance of 
        /// </summary>
        public UserController(ISecurityManager securityManager)
        {
            _securityManager = securityManager;
        }
        #endregion Constructors
        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="user"></param>
        [Route("users"),HttpPost]
        public async Task<HttpResponseMessage> AddUser(UserDTO userDTO)
        {
            string name = Request.GetRequestContext().Principal.Identity.Name;
            // Convert DTO to domain object
            // TODO: Use AutoMapper
            User user = GetUserFromDTO(userDTO);

            // Call into manager to save the user
            ResponseBase response = await _securityManager.Save(user);

            return HandlePostResponse(response, user, userDTO);
        }


        /// <summary>
        /// Modified user based on the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("users/{userId}"),HttpPut]
        public HttpResponseMessage UpdateUser(string userId, UserDTO user)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Deletes the user from the system
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <returns></returns>
        [Route("users/{userId}"),HttpDelete]
        public HttpResponseMessage DeleteUser(string userId)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Determines if a user exists with the supplied property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        [Route("users/exists/{propertyName}/{propertyValue}"), HttpGet]
        public async Task<HttpResponseMessage> IsUserExists(string propertyName, string propertyValue)
        {
            // Call the server
            var response = await _securityManager.UserPropertyExists(propertyName, propertyValue);
            return CreateUserExistsResponse(response,propertyName,propertyValue);
        }

        

        #region Support Methods
        /// <summary>
        /// This method maps User DTO to domain object. Use AutoMapper here later
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        private DataObjects.User GetUserFromDTO(UserDTO userDTO)
        {
            User user = new User();
            user.AuthenticationType = userDTO.AuthenticationType;
            user.Email = userDTO.Email;
            user.Password = userDTO.Password;
            user.UserName = userDTO.UserName;
            user.Name = userDTO.Name; ;

            return user;
        }

        /// <summary>
        /// This method handles the post response to be sent to user agent
        /// </summary>
        /// <param name="response">Response from business layer.Contains failures, if any and status</param>
        /// <param name="user">User domain object</param>
        /// <param name="userDTO">User DTO</param>
        /// <returns>HttpResponseMessage</returns>
        private HttpResponseMessage HandlePostResponse(ResponseBase response, User user, UserDTO userDTO)
        {
            if (response.Success)
            {
                userDTO.SerializePassword = false;
                return CreatePostSuccessMessage<UserDTO>("/users/" + user.Id.ToString(), userDTO);
            }
            else
            {
                // TODO: Failure response
                // 400 - Bad request
                // 500 - Internal server error
                return Create400BadRequestMessage("");
            }
        }

        /// <summary>
        /// Creates a http response for user exists
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private HttpResponseMessage CreateUserExistsResponse(ResponseBase response, string propertyName, string propertyValue)
        {
            UserPropertyValueExistsDTO dto = new UserPropertyValueExistsDTO();
            dto.Name = propertyName;
            dto.Value = propertyValue;
            dto.Exists = response.Success ? false : true;
            HttpResponseMessage message = Request.CreateResponse<UserPropertyValueExistsDTO>(HttpStatusCode.OK, dto);
            

            return message;

        }
        #endregion Support Methods

    }
}
