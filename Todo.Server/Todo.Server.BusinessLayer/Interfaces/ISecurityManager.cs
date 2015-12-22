using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer
{
    /// <summary>
    /// Interface defines methods for user management
    /// </summary>
    public interface ISecurityManager
    {
        /// <summary>
        /// This method will save the passed User object.
        /// </summary>
        /// <param name="user">The User object to save.</param>
        /// <returns></returns>
        Task<ResponseBase> Save(User user);
        
        /// <summary>
        /// Gets the user based on the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        ResponseBase GetUser(string userId, User user);

        /// <summary>
        /// Implements the login functionality for a user
        /// </summary>
        /// <param name="user">User to login</param>
        /// <returns>Response base containing failures, if any</returns>
        Task<LoginResponse> LoginAsync(User user);

        /// <summary>
        /// This method determines whether a user property is already present in the system
        /// </summary>
        /// <param name="property">Property name to check for</param>
        /// <param name="value">Property value to check for</param>
        /// <returns></returns>
        Task<ResponseBase> UserPropertyExists(string property, object value);


    }
}
