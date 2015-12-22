using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// This class represents a user.
    /// </summary>
    [Serializable]
    public class User : DataObjectBase
    {
        #region Constructors
        /// <summary>
        /// Constructor to create a new user.
        /// </summary>
        public User()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">the id</param>
        public User(Guid id)
            : base(id)
        {
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// gets/sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the user name.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets/sets the user email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets/Sets the authentication type
        /// </summary>
        public AuthType AuthenticationType { get; set; }

        /// <summary>
        /// Gets/sets the password
        /// </summary>
        [BsonIgnore]
        public string Password { get; set; }

        /// <summary>
        /// Gets/sets the user salt.
        /// </summary>
        public string  UserSalt { get; set; }

        /// <summary>
        /// Gets/sets the password hash
        /// </summary>
        public string PasswordHash { get; set; }
        #endregion Properties
    }
}
