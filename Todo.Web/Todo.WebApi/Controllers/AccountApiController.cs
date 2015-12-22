using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

using DataObjects;
using Server.BusinessLayer;
using Web.Common;
using System.Threading.Tasks;
using Web.Common;

namespace WebApi.Controllers
{
    /// <summary>
    /// Api controller for account management. Login/logoff/change password
    /// </summary>
    [RoutePrefix("api/account")]
    public class AccountApiController: ApiControllerBase
    {
        #region Members
        private ISecurityManager _securityManager;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Default constructor. IoC container will call this when unable to resolve dependencies.
        /// Set up the dependencies manually
        /// </summary>
        public AccountApiController():base()
        {
            _securityManager = new SecurityManager(ServerContext);
        }

        /// <summary>
        /// Constructor which declares the dependency. IoC container will call this when it has successfully resolved dependency and injected.
        /// </summary>
        /// <param name="securityManager"></param>
        public AccountApiController(ISecurityManager securityManager)
        {
            _securityManager = securityManager;
        }
        #endregion Constructors

        #region Api Methods
        /// <summary>
        /// Api method to be called when user is login to the application
        /// </summary>
        /// <returns></returns>
        [Route("login"),HttpPost]
        public async Task<HttpResponseMessage> Login(LoginRequestDTO loginRequest)
        {
            var user = CreateUserFromDTO(loginRequest);
            // invoke the server to login the user.
            LoginResponse loginResponse = await _securityManager.LoginAsync(user);

            return HandleLoginResponse(loginResponse, loginRequest);
            
        }

        #endregion Api Methods

        #region Support Methods
        /// <summary>
        /// Creates User domain object from the DTO
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        private User CreateUserFromDTO(LoginRequestDTO loginRequest)
        {
            //TODO: AutoMapper here
            User user = new User();
            user.UserName = loginRequest.Username;
            user.Password = loginRequest.Password;

            return user;
        }

        /// <summary>
        /// Creates the HTTP response based on the response from server layer
        /// </summary>
        /// <param name="loginResponse">Login response from server</param>
        /// <param name="loginRequest">Login request DTO</param>
        /// <returns>HttpResponseMessage</returns>
        private HttpResponseMessage HandleLoginResponse(LoginResponse loginResponse, LoginRequestDTO loginRequest)
        {
            // check the response if ok return success and set the form cookie
            if (loginResponse.Success)
            {
                HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.OK, GetUserDTO(loginResponse.User));
                // set the auth cookie
                FormsAuthentication.SetAuthCookie(loginResponse.User.UserName, true);

                return responseMessage;
            }
            else
            {
                // if error, send appropriate error.
                // 401 - incorrect login
                HttpResponseMessage responseMessage = Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Login Failed");
                return responseMessage;
                // TODO: 400 - bad request for model validation failures, 500 - server error
            }
        }

        /// <summary>
        /// Gets user dto from user domain object
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private UserDTO GetUserDTO(User user)
        {
            // TODO: AutoMapper here
            UserDTO userDTO = new UserDTO();
            userDTO.Id = user.Id.ToString();
            userDTO.UserName = user.UserName;
            userDTO.Email = user.Email;

            return userDTO;
        }
        
        #endregion Support Methods
    }
}
