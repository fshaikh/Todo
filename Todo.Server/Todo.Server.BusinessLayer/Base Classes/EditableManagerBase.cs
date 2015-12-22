using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer
{
    public abstract class EditableManagerBase<T> : ManagerBase where T:DataObjectBase
    {
        #region Constructors
        public EditableManagerBase(IServerContext serverContext)
            : base(serverContext)
        {
        }
        #endregion Constructors
        #region Abstract Methods
        /// <summary>
        /// This method performs any pre save operations. For eg: firing any events, etc
        /// </summary>
        /// <param name="user"></param>
        protected virtual void PerformPreSaveOperations(T dataObject)
        {
        }

        protected virtual void PerformPostSaveOperations()
        {
        }

        #endregion Abstract Methods
    }
}
