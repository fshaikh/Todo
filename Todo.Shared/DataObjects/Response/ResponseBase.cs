using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Base class for all responses to be returned from business layer. Contains collection of failures and success/failure status
    /// </summary>
    [Serializable]
    public class ResponseBase
    {
        #region Constructors
        public ResponseBase()
        {
            Failures = new List<IFailure>();
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Whether the operation is a success
        /// </summary>
        public Boolean Success
        {
            get
            {
                return (Failures == null) || (Failures.Count == 0);
            }
        }

        /// <summary>
        /// Gets/sets the failures
        /// </summary>
        public List<IFailure> Failures { get; set; }

        #endregion Properties
    }
}
