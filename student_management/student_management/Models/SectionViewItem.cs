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
                case '0': Term = "Spring"; break;
                case '1': Term = "Summer"; break;
                case '2': Term = "Fall"; break;
            }

            ClassID = s.ClassID;
            Year = s.AcademicYear;
        }
        public string ClassID { get; set; }
        public string CourseName { get; set; }
        public string Term { get; set; }
        public string Year { get; set; }
    }
}
