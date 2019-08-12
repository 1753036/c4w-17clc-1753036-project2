using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.DataAccess
{
    class DbConnection
    {
        private OleDbConnection connection = null;
        private static DbConnection dbconn = null;

        private DbConnection()
        {
            connection = new OleDbConnection();
            connection.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-U9H9TRM;Database=university;Trusted_Connection=yes";
            connection.Open();
        }

        public static DbConnection Instance()
        {
            if (dbconn == null)
            {
                dbconn = new DbConnection();
            }

            return dbconn;
        }

        public OleDbCommand SqlCommand(string sqlcommand)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = SqlConnection();
            cmd.CommandText = sqlcommand;
            return cmd;
        }

        public OleDbCommand SqlCommand(string sqlcommand, params object[] parameters)
        {
            OleDbCommand cmd = SqlCommand(sqlcommand);

            for (int i = 0; i < parameters.Count(); ++i)
            {
                cmd.Parameters.Add(new OleDbParameter(i.ToString(), parameters[i]));
            }

            return cmd;
        }

        public OleDbConnection SqlConnection()
        {
            return connection;
        }

        public void CleanUp()
        {
            SqlCommand("DELETE grade_report; DELETE section; DELETE course; DELETE student; DELETE class; DELETE auth").ExecuteNonQuery();
            SqlCommand("INSERT INTO auth VALUES ('giaovu', CONVERT(VARCHAR(32), HASHBYTES('MD5', 'giaovu'), 2), 'staff')").ExecuteNonQuery();
        }
    }
}
