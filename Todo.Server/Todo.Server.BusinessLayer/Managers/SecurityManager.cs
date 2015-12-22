using DataObjects;
using Server.DataAccess;
using Shared.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.BusinessLayer
{
    /// <summary>
    /// Manager class for user. Exposes methods for CRUD user, login/logoff, and other business functionality related to user and account management,.etc.
    /// </summary>
    public class SecurityManager : EditableManagerBase<User>, ISecurityManager
    {
        
        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="SecurityManager"/>
        /// </summary>
        /// <param name="connectionInfo"></param>
        public SecurityManager(IServerContext serverContext)
            : base(serverContext)
        {
            UserDataAccess = new UserDA(serverContext.ConnectionInfo);
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public UserDA UserDataAccess
        {
            get;
            private set;
        }
        #endregion Properties

        #region Methods

        #region ISecurityManager Methods
        /// <summary>
        /// This method will save the passed User object.
        /// </summary>
        /// <param name="user">The User object to save.</param>
        /// <returns></returns>
        public async Task<ResponseBase> Save(User user)
        {
            // TODO: Handle exception. For eg: throw if user is empty

            // Validate the model
            bool isSaveRequired = false;
            List<IFailure> failures = new List<IFailure>();
            isSaveRequired = PerformPreSaveValidations(user, failures);
            // if error return
            if (!isSaveRequired)
            {
                ResponseBase response = new ResponseBase();
                response.Failures = failures;

                return response;
            }

            // compute the salt and hash of the password and save to user model
            PerformPreSaveOperations(user);

            // call save on the repository
            var result  = await this.UserDataAccess.Save(user);

            // and post save operations
            PerformPostSaveOperations();

            return new ResponseBase();
        }


        /// <summary>
        /// Gets the user based on the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResponseBase GetUser(string userId, User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements the login functionality for a user
        /// </summary>
        /// <param name="user">User to login</param>
        /// <returns>Response base containing failures, if any</returns>
        public async Task<LoginResponse> LoginAsync(User user)
        {
            // TODO: Handle exception. For eg: throw if user is empty

            // TODO: model validation

            // get the user from the database
            var dbUser = await UserDataAccess.Get(user.UserName);
            
            // if no such user exists, return failure
            if (dbUser == null)
            {
                return CreateInvalidLoginResponse(user);
            }

            // compute the hash of the password and compare the hash with the one in the database
            bool isValid = PasswordHelper.IsPasswordValid(dbUser.PasswordHash, dbUser.UserSalt, user.Password);
            if (isValid)
            {
                // success, return 
                return new LoginResponse(dbUser);
            }
            else
            {
                // return 401
                return CreateInvalidLoginResponse(dbUser);
            }

            
        }

        /// <summary>
        /// This method determines whether a user property is already present in the system
        /// </summary>
        /// <param name="property">Property name to check for</param>
        /// <param name="value">Property value to check for</param>
        /// <returns></returns>
        public async Task<ResponseBase> UserPropertyExists(string property, object value)
        {
            // TODO: Handle exception. For eg: throw if user is empty

            var userExists = await UserDataAccess.IsPropertyValueExists(property, value);
            ResponseBase response = new ResponseBase();
            if (userExists)
            {
                response.Failures.Add(new PropertyValueNotUniqueFailure(DataObjectType.User, "", property, (string)value));
            }
            return response;
        }

        #endregion ISecurityManager Methods

        #region Override Methods

        /// <summary>
        /// This method performs any pre save operations. For eg: firing any events, etc
        /// </summary>
        /// <param name="user"></param>
        protected override void PerformPreSaveOperations(User user)
        {
            // compute the salt and hash of the password
            ComputePasswordHash(user);
        }

        protected override void PerformPostSaveOperations()
        {

        }
        #endregion Override Methods

        #region Validation Methods
        /// <summary>
        /// This method performs business rules validations on the user object.Returns the failures
        /// </summary>
        /// <param name="user"></param>
        /// <param name="failures"></param>
        /// <returns></returns>
        private bool PerformPreSaveValidations(User user, List<IFailure> failures)
        {
            bool isValid = true;
            // if the user is being deleted, just return cause we dont want to do any validation
            if (user.IsDeleted)
                return isValid;

            // if the user is being created
            if (user.IsNew)
            {
                // Check all mandatory values
                CheckMandatoryValues(user, failures);
                // Check if the user name is unique
                // check if the email address is unique
                CheckUniqueValues(user, failures);
                // check if the password meets complexity requirement
                CheckPasswordComplexity(user, failures);
                // TODO: Other checks
            }
            return failures.Count == 0;

        }

       

       

        /// <summary>
        /// Checks if all the mandatory values are set.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="failures"></param>
        private void CheckMandatoryValues(User user, List<IFailure> failures)
        {
            // username
            IFailure failure = CheckPropertyValueSet("UserName", user.UserName);
            failures.AddIfNotNull(failure);
            // email address
            failure = CheckPropertyValueSet("Email", user.Email);
            failures.AddIfNotNull(failure);
            // password
            failure = CheckPropertyValueSet("Password", user.Password);
            failures.AddIfNotNull(failure);
            // name
            failure = CheckPropertyValueSet("Name", user.Name);
            failures.AddIfNotNull(failure);
        }

        /// <summary>
        /// Checks if the required properties have unique values
        /// </summary>
        /// <param name="user"></param>
        /// <param name="failures"></param>
        private void CheckUniqueValues(User user, List<IFailure> failures)
        {
            // Check if the user name is unique
            
            // check if the email address is unique
        }

        private IFailure CheckPropertyValueSet(string propertyName, string propertyValue)
        {
            IFailure failure = null;
            if (string.IsNullOrEmpty(propertyValue))
            {
                failure = new PropertyValueNotSetFailure(DataObjectType.User, propertyValue);
            }
            return failure;
        }

        private void CheckPasswordComplexity(User user, List<IFailure> failures)
        {
            
        }
        #endregion Validation Methods

        #region Support Methods

       

        /// <summary>
        /// Computes the password hash and user salt
        /// </summary>
        /// <param name="user"></param>
        private void ComputePasswordHash(User user)
        {
            byte[] saltBytes = PasswordHelper.GetUniqueSalt();
            user.UserSalt = PasswordHelper.GetBase64EncodedValue(saltBytes);

            byte[] passwordHashBytes = PasswordHelper.GetHashedPassword(user.Password, user.UserSalt);
            user.PasswordHash = PasswordHelper.GetBase64EncodedValue(passwordHashBytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private LoginResponse CreateInvalidLoginResponse(User user)
        {
            LoginResponse responseBase = new LoginResponse(user);
            responseBase.Failures.Add(new InvalidLoginCredentialsFailure(user.UserName));
            return responseBase;
        }

        #endregion Support Methods

        #endregion Methods








        
    }
}
