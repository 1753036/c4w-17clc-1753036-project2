using student_management.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.DataAccess
{
    public class AuthRepo
    {
        public bool Check(string username, string password)
        {
            Database.Open();
            OleDbCommand cmd = Database.Command("SELECT dbo.fn_authorize(?,?)");
            cmd.Parameters.Add(new OleDbParameter("username", username));
            cmd.Parameters.Add(new OleDbParameter("password", password));
            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }

        public string GetPermission(string username, string password)
        {
            if (Check(username, password) == false)
            {
                return "";
            }

            Database.Open();
            OleDbCommand cmd = Database.Command("SELECT permission FROM auth WHERE username=?");
            cmd.Parameters.Add(new OleDbParameter("username", username));
            OleDbDataReader rd = cmd.ExecuteReader();
            rd.Read();
            return rd.GetString(0);
        }
    }
}
