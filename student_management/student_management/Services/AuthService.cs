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
            if (repo.Check(username, password))
            {
                auth = new Auth();
                auth.Username = username;
                auth.Password = password;
                auth.Permission = repo.GetPermission(username, password);
            }
            return auth;
        }
    }
}
