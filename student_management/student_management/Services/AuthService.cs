using student_management.DataAccess;
using student_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Services
{
    class AuthService
    {
        AuthRepo repo = new AuthRepo();
        public Auth Authorize(string username, string password)
        {
            Auth auth = null;
            PasswordHash hash = new PasswordHash(password);
            if (repo.Check(username, hash.Hash()))
            {
                auth = new Auth();
                auth.Username = username;
                auth.Password = hash.Hash();
                auth.Permission = repo.GetPermission(username, hash.Hash());
            }
            return auth;
        }

        public bool ChangePassword(string username, string password, string newPassword)
        {
            PasswordHash oldHash = new PasswordHash(password);
            PasswordHash newHash = new PasswordHash(newPassword);
            if (repo.Check(username, oldHash.Hash()))
            {
                
                repo.ChangePassword(username, oldHash.Hash(), newHash.Hash());
                return true;
            }

            return false;
        }
    }
}
