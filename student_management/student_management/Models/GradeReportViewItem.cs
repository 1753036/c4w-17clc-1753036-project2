using student_management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Models
{
    public class GradeReportViewItem
    {
        public GradeReportViewItem(GradeReport report, Section section)
        {
            var courseService = new CourseService();
            var classService = new ClassService();
            SectionName = section.CourseID + " - " + courseService.GetCourse(section.CourseID).Fullname;
            StudentID = report.StudentID;
            Fullname = classService.GetStudent(report.StudentID).Fullname;
            Midterm = report.Midterm;
            Final = report.Final;
            Other = report.Other;
            Total = report.Total;
        }

        public string SectionName { get; set; }
        public string StudentID { get; set; }
        public string Fullname { get; set; }
        public double Midterm { get; set; }
        public double Final { get; set; }
        public double Other { get; set; }
        public double Total { get; set; }
    }
}
