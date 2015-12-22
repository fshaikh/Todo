using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Infrastructure;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Enable attribute based routing
            GlobalConfiguration.Configure(x => x.MapHttpAttributeRoutes());

            // Enable JSON for request/response payload
            config.Services.Replace(typeof(IContentNegotiator),new JsonOnlyContentNegotiator(new JsonMediaTypeFormatter()));
            
            // Enable CORS
            var cors = new EnableCorsAttribute(ConfigurationHandler.GetCrossOriginUrl(), "*", "*");
            config.EnableCors(cors);
        }
    }
}
