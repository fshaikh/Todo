using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Web.Http;
using Server.BusinessLayer;
using WebApi.Infrastructure;
using System.Net;

namespace WebApi.Controllers
{
    /// <summary>
    /// Base class for all api contollers classes.Defines methods for handling errors, responses, authentication ,etc
    /// </summary>
    public abstract class ApiControllerBase : ApiController
    {
        #region Members
        private IServerContext _serverContext;
        #endregion Members

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        protected ApiControllerBase()
        {
            _serverContext = new ServerContext(null, ConfigurationHandler.GetConnectionString(), "TodoDb");
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the Server Context. Exposed so that all API controller classes can use it. 
        /// </summary>
        protected IServerContext ServerContext
        {
            get
            {
                return _serverContext;
            }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Creates a success HTTP response for a POST operation:
        ///     201 status code
        ///     Location header set to the url of the new user
        ///     Same JSON in the response body
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="location"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected HttpResponseMessage CreatePostSuccessMessage<T>(string location, T item)
        {
            // create success response for POST
            HttpResponseMessage responseMessage = Request.CreateResponse<T>(HttpStatusCode.Created, item);
            responseMessage.Headers.Add(Constants.LOCATION_HEADER, location);

            return responseMessage;
            
        }

        /// <summary>
        /// Creates a success HTTP response for a DELETE operation.
        ///     200 OK
        ///     TODO: What else needs to be returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        protected HttpResponseMessage CreateDeleteSuccessMessage<T>(T item)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        /// <summary>
        /// Creates a 400 - bad request message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected HttpResponseMessage Create400BadRequestMessage(string message)
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
        }
        #endregion Methods
    }
}
