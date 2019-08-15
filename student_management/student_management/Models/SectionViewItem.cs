using student_management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Models
{
    public class SectionViewItem
    {
        public SectionViewItem(Section s)
        {
            CourseService courseService = new CourseService();
            SectionService sectionService = new SectionService();
            Course course = courseService.GetCourse(s.CourseID);
            if (course != null)
            {
                CourseName = course.Fullname;
            }

            switch (s.Term)
            {
                case '0': Term = "HK1"; break;
                case '1': Term = "HK2"; break;
                case '2': Term = "HK3"; break;
            }

            ClassID = s.ClassID;
            Year = s.AcademicYear;
        }
        public string ClassID;
        public string CourseName;
        public string Term;
        public string Year;
    }
}
