using student_management.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.DataAccess
{
    public class CourseRepo
    {
        private DbConnection dbconn = DbConnection.Instance();
        public Course AddCourse(string id, string fullname, string room)
        {
            var cmd = dbconn.SqlCommand(
                "INSERT INTO course VALUES(?,?,?)",
                id, fullname, room
            );
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return null;
            }

            Course course = new Course();
            course.ID = id;
            course.Fullname = fullname;
            course.Room = room;
            return course;
        }

        public Course GetCourse(string courseID)
        {
            var cmd = dbconn.SqlCommand("SELECT * FROM course WHERE id=?", courseID);
            var rd = cmd.ExecuteReader();
            Course course = null;
            while (rd.Read())
            {
                course = CourseReader.Read(ref rd);
            }
            return course;
        }

        public List<Course> GetListCourses()
        {
            List<Course> listCourses = new List<Course>();
            var cmd = dbconn.SqlCommand("SELECT * FROM course");
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                listCourses.Add(CourseReader.Read(ref rd));
            }
            return listCourses;
        }
    }
}
