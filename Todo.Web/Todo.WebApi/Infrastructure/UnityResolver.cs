using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using WebApi;

namespace WebApi.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class UnityResolver :IDependencyResolver
    {
        #region Members
        protected IUnityContainer _unityContainer;
        #endregion Members

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public UnityResolver(IUnityContainer container)
        {
            _unityContainer = container;
        }

        #endregion Constructors

        #region IDependencyResolver Methods
        public IDependencyScope BeginScope()
        {
            var child = _unityContainer.CreateChildContainer();
            return new UnityResolver(child);
        }

        /// <summary>
        /// When ASP.NET Web API runtime creates a controller instance, it calls GetService method passing controller type 
        /// to the GetService method. This method should inject the requisite dependency.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return _unityContainer.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                // always return null. Never throw.
                return null;
            }
        }

        /// <summary>
        /// Same as above, but returns a collection of dependencies.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _unityContainer.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                // always return empty collection. Never throw.
                return new List<object>();
            }
        }

        /// <summary>
        /// Disposes the resources , if any
        /// </summary>
        public void Dispose()
        {
            _unityContainer.Dispose();
        }
        #endregion IDependencyResolver Methods
    }
}
