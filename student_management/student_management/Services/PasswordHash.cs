using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Services
{
    public class PasswordHash
    {
        private string mPassword;
        public PasswordHash(string password)
        {
            mPassword = password;
        }

        public string Hash()
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(mPassword);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToUpper();
        }
    }
}
