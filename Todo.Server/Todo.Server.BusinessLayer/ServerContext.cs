using DataObjects;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer
{
    /// <summary>
    /// Class used as context by all business layer classes.
    /// </summary>
    public class ServerContext : IServerContext
    {
        #region Members
        private NameValueCollection _appSettings;
        private ConnectionInfo _connectionInfo;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ServerContext"/>
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="connectionString"></param>
        public ServerContext(NameValueCollection appSettings, string connectionString,string databaseName)
        {
            _appSettings = appSettings;
            _connectionInfo = new ConnectionInfo();
            _connectionInfo.ConnectionString = connectionString;
            _connectionInfo.DatabaseName = databaseName;
        }
        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the connection info
        /// </summary>
        public ConnectionInfo ConnectionInfo
        {
            get
            {
                return _connectionInfo;
            }
        }

        public ISecurityManager UserManager
        {
            get { throw new NotImplementedException(); }
        }
        #endregion Properties
    }
}
