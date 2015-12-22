using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;
using Newtonsoft.Json;

namespace Web.Common
{
    /// <summary>
    /// Represents the user DTO
    /// </summary>
    public class UserDTO : BaseDTO
    {
        #region Members
        [JsonIgnore]
        private bool _serializePassword = true;
        #endregion Members

        #region DTO Properties

        /// <summary>
        /// gets/sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets/sets the email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Get/sets the user password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Get/sets the auth type
        /// </summary>
        public AuthType AuthenticationType { get; set; }

        /// <summary>
        /// Get/sets whether to serialize password or not. If true, password will be serialized, else false
        /// </summary>
        [JsonIgnore]
        public bool SerializePassword
        {
            get
            {
                return _serializePassword;
            }
            set
            {
                _serializePassword = value;
            }
        }

        #endregion DTO Properties

        #region Methods
        /// <summary>
        /// This method is invoked by JSON.NET (dwfault JSON formatter) when serializing/deserializing Password. Since we do not want to send password to client
        /// while serializing to JSON, we return false. 
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializePassword()
        {
            return (this._serializePassword);
        }
        #endregion Methods
    }
}
