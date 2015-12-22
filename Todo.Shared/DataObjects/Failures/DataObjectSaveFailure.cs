using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Represents failure object for data object saves
    /// </summary>
    [Serializable]
    public class DataObjectSaveFailure<T> :IFailure where T:DataObjectBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="DataObjectSaveFailure"/>
        /// </summary>
        /// <param name="dataObject"></param>
        /// <param name="saveType"></param>
        /// <param name="exception"></param>
        public DataObjectSaveFailure(T dataObject, SaveType saveType, Exception exception = null)
        {
            DataObject = dataObject;
            SaveType = saveType;
            Exception = exception;
        }
        #endregion Constructors
        #region Properties
        public T DataObject
        {
            get;
            private set;
        }

        public SaveType SaveType 
        { 
            get;
            private set;
        }

        public Exception Exception
        {
            get;
            private set;
        }
        #endregion Properties
    }
}
