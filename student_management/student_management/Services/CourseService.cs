using student_management.DataAccess;
using student_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Services
{
    public class CourseService
    {
        CourseRepo repo = new CourseRepo();

        public Course AddCourse(string id, string fullname, string room)
        {
            return repo.AddCourse(id, fullname, room);
        }

        public Course AddCourse(Course course)
        {
            return AddCourse(course.ID, course.Fullname, course.Room);
        }

        public Course GetCourse(string courseID)
        {
            return repo.GetCourse(courseID);
        }

        public List<Course> GetListCourses()
        {
            return repo.GetListCourses();
        }
    }
}
