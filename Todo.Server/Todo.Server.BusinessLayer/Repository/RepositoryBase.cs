using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer
{
    public abstract class RepositoryBase
    {
        #region Constructors
        protected RepositoryBase(IServerContext serverContext)
        {
            ServerContext = serverContext;
        }
        #endregion Constructors

        #region Properties
        protected IServerContext ServerContext
        {
            get;
            private set;
        }

        // Cache manager/facade object should come here.
        #endregion Properties
    }
}
