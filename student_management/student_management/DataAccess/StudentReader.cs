using student_management.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.DataAccess
{
    public class StudentReader
    {
        public static Student Read(ref OleDbDataReader rd)
        {
            var student = new Student();
            student.ID = rd.GetString(0);
            student.Fullname = rd.GetString(1);
            student.Gender = rd.GetString(2)[0];
            student.Birthday = rd.GetString(3);
            student.SocialID = rd.GetString(4);
            student.ClassID = rd.GetString(5);
            return student;
        }
    }
}
