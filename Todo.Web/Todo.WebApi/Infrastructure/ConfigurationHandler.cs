using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Infrastructure
{
    /// <summary>
    /// Class to expose properties from web.config
    /// </summary>
    public class ConfigurationHandler
    {
        #region Constants
        private const string MONGODB_CONNECTIONSTRING_KEY = "MongoDbConnectionString";
        private const string CROSSORIGINURL_KEY = "CrossOriginUrl";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Gets the connection string from config file
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[MONGODB_CONNECTIONSTRING_KEY].ConnectionString;
        }

        /// <summary>
        /// Gets the cross origin url to allow access to 
        /// </summary>
        /// <returns></returns>
        public static string GetCrossOriginUrl()
        {
            return ConfigurationManager.AppSettings[CROSSORIGINURL_KEY];
        }
        #endregion Methods
    }
}
