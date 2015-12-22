using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Represents failure object when the username does not exist
    /// </summary>
    [Serializable]
    public class InvalidLoginCredentialsFailure : IFailure
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="InvalidLoginCredentialsFailure"/>
        /// </summary>
        /// <param name="userName"></param>
        public InvalidLoginCredentialsFailure(string userName)
        {
            UserName = userName;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the user name 
        /// </summary>
        public string UserName { get; private set; }
        #endregion Properties
    }
}
