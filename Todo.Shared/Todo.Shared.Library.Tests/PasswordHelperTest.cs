using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Todo.Shared.Library.Tests
{
    [TestClass]
    public class PasswordHelperTest
    {
        [TestMethod]
        public void IsPasswordValid_Test()
        {
            string password = "test1234";

            byte[] salt = PasswordHelper.GetUniqueSalt();
            byte[] hashedPasswordBytes = PasswordHelper.GetHashedPassword(password, salt);
            string hashedPassword = PasswordHelper.GetBase64EncodedValue(hashedPasswordBytes);

            bool isValid = PasswordHelper.IsPasswordValid(hashedPassword, salt, password);
            Assert.IsTrue(isValid);


        }

        [TestMethod]
        public void IsPasswordNotValid_Test()
        {
            string password = "test1234";

            byte[] salt = PasswordHelper.GetUniqueSalt();
            byte[] hashedPasswordBytes = PasswordHelper.GetHashedPassword(password, salt);
            string hashedPassword = PasswordHelper.GetBase64EncodedValue(hashedPasswordBytes);

            password = "test12345";
            bool isValid = PasswordHelper.IsPasswordValid(hashedPassword, salt, password);
            Assert.IsTrue(!isValid);


        }
    }
}
