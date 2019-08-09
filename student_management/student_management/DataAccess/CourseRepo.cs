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
        public void AddCourse(Course newCourse)
        {
            Database.Open();
            var cmd = Database.Command("INSERT INTO course VALUES(?,?,?)");
            cmd.Parameters.Add(new OleDbParameter("id", newCourse.ID));
            cmd.Parameters.Add(new OleDbParameter("name", newCourse.Fullname));
            cmd.Parameters.Add(new OleDbParameter("room", newCourse.Room));
            cmd.ExecuteNonQuery();
        }

        public Course GetCourse(string id)
        {
            Database.Open();
            var cmd = Database.Command("SELECT * FROM course WHERE id=?");
            cmd.Parameters.Add(new OleDbParameter("id", id));
            var rd = cmd.ExecuteReader();
            Course course = null;
            while (rd.Read())
            {
                course = new Course();
                course.ID = rd.GetString(0);
                course.Fullname = rd.GetString(1);
                course.Room = rd.GetString(2);
            }
            return course;
        }

        public List<Course> GetListCourses()
        {
            return new List<Course>();
        }
    }
}
