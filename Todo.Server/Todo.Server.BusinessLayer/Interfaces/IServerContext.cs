using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer
{
    /// <summary>
    /// Interface to expose all business server related objects : managers, contexts, transaction methods
    /// </summary>
    public interface IServerContext
    {
        #region Properties
        /// <summary>
        /// Gets the connection info 
        /// </summary>
        ConnectionInfo ConnectionInfo
        {
            get;
        }

        #region Managers
        /// <summary>
        /// Gets the User manager
        /// </summary>
        ISecurityManager UserManager
        { 
            get;
        }
        #endregion Managers
        #endregion Properties
    }
}
