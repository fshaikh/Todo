using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.DataAccess;
using DataObjects;
using Shared.Library;


namespace DataAccess.Tests
{
    [TestClass]
    public class DataAccessUserUnitTest:DataAccessTestBase
    {
        [TestMethod]
        public void DataAccess_User_Insert()
        {
            UserDA userDA = new UserDA(GetConnectionInfo());

            User user = GetNewUser();

            userDA.Save(user);
            //Console.ReadLine();
        }

        [TestMethod]
        public void DataAccess_User_IsUserExists()
        {
            UserDA userDA = new UserDA(GetConnectionInfo());
            string username = "fshaikh1";

            bool isUserExists = userDA.IsUserExists(username).Result;
            Assert.IsTrue(isUserExists);
        }

        [TestMethod]
        public void DataAccess_User_GetUserBasedOnUserName()
        {
            UserDA userDA = new UserDA(GetConnectionInfo());
            string username = "fshaikh1";

            User user = userDA.Get(username).Result;
            Assert.IsNotNull(user,"Failed to load user with user name: " + username);
        }

        private User GetNewUser()
        {
            User user = new User();
            string password = "test1234";
            byte[] salt = GetSalt();
            user.UserName = "fshaikh";
            user.AuthenticationType = AuthType.Custom;
            user.Email = "fshaikh@cc.com";
            user.UserSalt = PasswordHelper.GetBase64EncodedValue(salt);
            user.PasswordHash = GetPasswordHash(password, user.UserSalt);

            return user;
        }

        private string GetPasswordHash(string password, string salt)
        {
            byte[] hash = PasswordHelper.GetHashedPassword(password, salt);
            return PasswordHelper.GetBase64EncodedValue(hash);
        }

        private byte[] GetSalt()
        {
            return PasswordHelper.GetUniqueSalt();
        }
    }
}
