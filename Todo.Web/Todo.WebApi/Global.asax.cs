using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using WebApi.Infrastructure;

namespace WebApi
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //System.Diagnostics.Debugger.Break();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            // Register the IoC container
            //RegisterDIContainer(GlobalConfiguration.Configuration);

        }

        private void RegisterDIContainer(HttpConfiguration configuration)
        {
            // Create an IoC Container
            var container = new UnityContainer();
            // Load the configuration into the container. Configuration has been defined at design time in web.config under <unity> section.
            container.LoadConfiguration();
            // Set the dependency resolver. UnityResolver is our custom resolver implementing IDependencyResolver provided by Web API.
            // IDependencyResolver acts as a bridge between Web API and IoC Container
            configuration.DependencyResolver = new UnityResolver(container);
        }

        
    }
}