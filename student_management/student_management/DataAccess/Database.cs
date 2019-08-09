using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.DataAccess
{
    class Database
    {
        private static OleDbConnection conn = null;
        public static OleDbConnection Open()
        {
            if (conn == null)
            {
                conn = new OleDbConnection();
                conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-CCTSD12;Database=university;Trusted_Connection=yes";
                conn.Open();
            }
            
            return conn;
        }

        public static void Close()
        {
            conn.Close();
        }

        public static OleDbCommand Command(string command)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = command;
            return cmd;
        }
    }
}
