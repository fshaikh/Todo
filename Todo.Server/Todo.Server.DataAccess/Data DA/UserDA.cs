using DataObjects;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccess
{
    /// <summary>
    /// This class represents Data access functionality for User object
    /// </summary>
    public class UserDA: DataAccessBase<User>
    {
        #region Constants
        private const string USER_COLLECTION_NAME = "User";
        #endregion Constants

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="UserDA"/>
        /// </summary>
        /// <param name="connectionInfo">Connection info</param>
        public UserDA(ConnectionInfo connectionInfo)
            : base(connectionInfo)
        {
        }
        #endregion Constructors

        #region Save Methods

        /// <summary>
        /// Saves a user object
        /// </summary>
        /// <param name="user">User object to save</param>
        public async override Task<bool> Save(User user)
        {
            if (user.IsNew)
            {
                var result = await Insert(user);
                return result;
            }
            if (user.IsChanged)
            {
            }
            if (user.IsDeleted)
            {
            }
            return true; ;
        }

        /// <summary>
        /// Inserts a new user
        /// </summary>
        /// <param name="user"></param>
        private async Task<bool> Insert(User user)
        {
            // Get the collection first
            var collection = Database.GetCollection<User>(USER_COLLECTION_NAME);
            if (collection != null)
            {
                await collection.InsertOneAsync(user);
                return true;
            }
            return false;
        }
        #endregion Save Methods

        #region Load Methods

        /// <summary>
        /// Gets a user based on user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>User</returns>
        public override async Task<User> Get(object userName)
        {
            User user = null;
            var collection = Database.GetCollection<User>(USER_COLLECTION_NAME);
            if (collection != null)
            {
                var filter = GetUsernameFilter((string)userName);
                var users = await collection.Find<User>(filter).ToListAsync();
                if (users.Count == 0 || users.Count > 1)
                    return user;
                user =  users[0];

            }
            return user;
        }

        /// <summary>
        /// Checks if the username exists in the collection.
        /// </summary>
        /// <param name="userName">user name to verify</param>
        /// <returns>true if user exists else false</returns>
        public async Task<bool> IsUserExists(string userName)
        {
            // Get the collection first
            long userCount = 0;
            var collection = Database.GetCollection<User>(USER_COLLECTION_NAME);
            if (collection != null)
            {
                userCount = await collection.CountAsync(GetUsernameFilter(userName));
            }
            return userCount > 0;

        }

        /// <summary>
        /// This method checks if the user collection has a user which matches the property value
        /// </summary>
        /// <param name="property">USer property name</param>
        /// <param name="value">Property value</param>
        /// <returns>true if the user exists else false</returns>
        public async Task<bool> IsPropertyValueExists(string property, object value)
        {
            long objectCount = 0;
            // Get the collection first
            var collection = Database.GetCollection<User>(USER_COLLECTION_NAME);
            if (collection != null)
            {
                objectCount = await collection.CountAsync(GetPropertyValueFilter(property, value));
            }
            return objectCount > 0;
        }
        #endregion Load Methods

        #region Support Methods
        /// <summary>
        /// Gets the user name filter 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private FilterDefinition<User> GetUsernameFilter(string userName)
        {
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Eq("UserName", userName);
            return filter;
        }

        /// <summary>
        /// Gets s generic user filter
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        private FilterDefinition<User> GetPropertyValueFilter(string propertyName, object propertyValue)
        {
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Eq(propertyName, propertyValue);
            return filter;
        }
        #endregion Support Methods
    }
}
