using System; 
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Infrastructure
{
    /// <summary>
    /// This class performs content negotiation. This is the process of selecting a response writer (formatter) in compliance with header values in the request.
    /// We are replacing the default conneg with a custom one. Since we support only JSON, we will add only JSON formatter
    /// <example></example>
    /// </summary>
    public class JsonOnlyContentNegotiator : IContentNegotiator
    {
        #region Members
        private JsonMediaTypeFormatter _jsonMediaTypeFormatter;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="JsonOnlyContentNegotiator".
        /// Json formatter is passed to prevent the formatter being created per request/>
        /// </summary>
        /// <param name="formatter">Json formatter</param>
        public JsonOnlyContentNegotiator(JsonMediaTypeFormatter formatter)
        {
            _jsonMediaTypeFormatter = formatter;
        }
        #endregion Constructors

        #region IContentNegotiator Methods
        /// <summary>
        /// Performs content negotiating by selecting the most appropriate System.Net.Http.Formatting.MediaTypeFormatter
        //     out of the passed in formatters for the given request that can serialize
        //     an object of the given type.
        /// </summary>
        /// <param name="type">The type to be serialized.</param>
        /// <param name="request"> Request message, which contains the header values used to perform negotiation.</param>
        /// <param name="formatters">The set of System.Net.Http.Formatting.MediaTypeFormatter objects from which to choose.</param>
        /// <returns>The result of the negotiation containing the most appropriate System.Net.Http.Formatting.MediaTypeFormatter
        //     instance, or null if there is no appropriate formatter.</returns>
        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            var result = new ContentNegotiationResult(_jsonMediaTypeFormatter, new MediaTypeHeaderValue(Constants.MEDIATYPE_JSON));
            return result;
        }
        #endregion IContentNegotiator Methods
    }
}
