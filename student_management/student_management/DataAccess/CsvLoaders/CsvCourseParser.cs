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
            if (Data[0].Count() != 4)
            {
                throw new CsvCourseWrongFormat("Csv file wrong format");
            }
        }

        public string GetClassName()
        {
            return Data[0][0];
        }

        public int GetNumCourse()
        {
            return Data.Count() - 2;
        }

        public List<Course> GetCourses()
        {
            List<Course> courses = new List<Course>();
            int num = GetNumCourse();

            for (int i = 0; i < num; ++i)
            {
                int index = i + 2;
                var course = new Course();
                course.ID = Data[index][1];
                course.Fullname = Data[index][2];
                course.Room = Data[index][3];
                courses.Add(course);
            }

            return courses;
        }
    }
}
