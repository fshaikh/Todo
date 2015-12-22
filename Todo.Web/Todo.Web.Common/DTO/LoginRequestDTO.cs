using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Common
{
    /// <summary>
    /// DTO for login request.
    /// </summary>
    public class LoginRequestDTO :BaseDTO
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginRequestDTO()
        {
            // do nothing
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets/sets the username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Gets/sets the password
        /// </summary>
        public string Password { get; set; }
        #endregion Properties
    }
}
