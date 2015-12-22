using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer
{
    /// <summary>
    /// Represents base class for all managers.
    /// </summary>
    public abstract class ManagerBase
    {
        #region Members
        private IServerContext _serverContext;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="serverContext"></param>
        protected ManagerBase(IServerContext serverContext)
        {
            _serverContext = serverContext;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets/sets the connection info
        /// </summary>
        public IServerContext ServerContext
        {
            get
            {
                return _serverContext;
            }
        }
        #endregion Properties

        
    }
}
