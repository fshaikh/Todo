using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Library
{
    /// <summary>
    /// This class implements methods related to generating unique salt for each user, password hashing,etc
    /// </summary>
    public class PasswordHelper
    {
        /// <summary>
        /// This method returns an unique salt for each user. Salt must be a cryptographically secured random number which must follow :
        ///   1. Must be CS random number
        ///   2. Must be unique.
        /// </summary>
        /// <param name="saltLength">Salt length</param>
        /// <returns>Unique salt value. Returned as a base64 encoded value.</returns>
        public static byte[] GetUniqueSalt(int saltLength = 10)
        {
            byte[] buffer = new byte[saltLength];
            using(RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider())
            {
                cryptoProvider.GetBytes(buffer);
                return buffer;
            }
        }

        /// <summary>
        /// Returns a base64 encoded string value for a given input array
        /// </summary>
        /// <param name="inputBytes">inoit bytes array</param>
        /// <returns>base64 encoded value</returns>
        public static string GetBase64EncodedValue(byte[] inputBytes)
        {
            return Convert.ToBase64String(inputBytes);
        }

        /// <summary>
        /// This method implements password hash using SHA256 and unique salt. Salt is unique to each user.
        /// </summary>
        /// <param name="saltBytes">Salt to use when hashing</param>
        /// <returns>Password hash</returns>
        public static byte[] GetHashedPassword(string password,string salt)
        {
            string passwordToHash = salt + password;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(passwordToHash);
            using (SHA256 hashAlgo = SHA256.Create())
            {
                // Combine the password and salt
                //byte[] combinedBytes = CombineBlocks(passwordBytes, saltBytes);
                return hashAlgo.ComputeHash(passwordBytes);
            }
        }

        /// <summary>
        /// This method validates the passed password with the hash value.
        /// </summary>
        /// <param name="hashedPassword"></param>
        /// <param name="salt"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsPasswordValid(string hashedPassword, string salt, string password)
        {
            // compute the hash of the passed in password with the passed hash value
            byte[] passwordToBeValidated = GetHashedPassword(password, salt);
            string base64Value = GetBase64EncodedValue(passwordToBeValidated);
            if (string.Compare(hashedPassword, base64Value) == 0)
                return true;
            return false;
        }

        /// <summary>
        /// Combines the 2 byte arrays into a new byte array
        /// </summary>
        /// <param name="password">password array</param>
        /// <param name="saltBytes">salt array</param>
        /// <returns></returns>
        private static byte[] CombineBlocks(byte[] password, byte[] saltBytes)
        {
            byte[] combinedBytes = new byte[password.Length + saltBytes.Length];
            Buffer.BlockCopy(password, 0, combinedBytes, 0, password.Length);
            Buffer.BlockCopy(saltBytes, 0, combinedBytes, password.Length, saltBytes.Length);
            return combinedBytes;
        }



        public static string GetPasswordPBKDF(string password)
        {
            throw new NotImplementedException("PBKDF2 id not yet implemented. Use GetHashedPassword instead");
        }
    }
}
