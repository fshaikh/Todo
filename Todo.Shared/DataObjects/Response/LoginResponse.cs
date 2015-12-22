using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Represents login response
    /// </summary>
    [Serializable]
    public class LoginResponse : ResponseBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="LoginResponse"/>
        /// </summary>
        /// <param name="user"></param>
        public LoginResponse(User user)
        {
            User = user;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the login user
        /// </summary>
        public User User { get; private set; }
        #endregion Properties
    }
}
