using student_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.CsvLoaders
{
    public class CsvCourseParser : CsvParser
    {
        public CsvCourseParser(string filename)
            : base(filename)
        {
            if (data[0].Count() != 4)
            {
                throw new CsvCourseWrongFormat("Csv file wrong format");
            }
        }

        public string GetClassName()
        {
            return data[0][0];
        }

        public int GetNumCourse()
        {
            return data.Count() - 2;
        }

        public List<Course> GetCourses()
        {
            List<Course> courses = new List<Course>();
            int num = GetNumCourse();

            for (int i = 0; i < num; ++i)
            {
                int index = i + 2;
                var course = new Course();
                course.ID = data[index][1];
                course.Fullname = data[index][2];
                course.Room = data[index][3];
                courses.Add(course);
            }

            return courses;
        }
    }
}
