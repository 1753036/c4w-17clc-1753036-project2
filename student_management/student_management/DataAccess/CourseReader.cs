using student_management.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.DataAccess
{
    public class CourseReader
    {
        public static Course Read(ref OleDbDataReader rd)
        {
            Course course = new Course();
            course.ID = rd.GetString(0);
            course.Fullname = rd.GetString(1);
            course.Room = rd.GetString(2);
            return course;
        }
    }
}
