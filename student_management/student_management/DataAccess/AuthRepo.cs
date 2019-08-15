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
        private DbConnection dbconn = DbConnection.Instance();
        public bool Check(string username, string password)
        {
            OleDbCommand cmd = dbconn.SqlCommand(
                "SELECT dbo.fn_authorize(?,?)", 
                username, 
                password
            );

            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }

        public string GetPermission(string username, string password)
        {
            if (Check(username, password) == false)
            {
                return "";
            }

            OleDbCommand cmd = dbconn.SqlCommand(
                "SELECT permission FROM auth WHERE username=?", 
                username
            );
            OleDbDataReader rd = cmd.ExecuteReader();
            rd.Read();
            return rd.GetString(0);
        }

        public bool ChangePassword(string username, string password, string newpassword)
        {
            if (Check(username, password) == false)
            {
                return false;
            }

            OleDbCommand cmd = dbconn.SqlCommand(
                "UPDATE auth SET passwd=? WHERE username=?",
                newpassword, username
            );

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
