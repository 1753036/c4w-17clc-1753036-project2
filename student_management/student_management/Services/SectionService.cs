using student_management.CsvLoaders;
using student_management.DataAccess;
using student_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Services
{
    public class SectionService
    {
        SectionRepo repo = new SectionRepo();

        public void ReadFromCsv(string filename)
        {
            CsvCourseParser parser = new CsvCourseParser(filename);

            string myclass = parser.GetClassName();
            string term = parser.GetTerm();
            string year = parser.GetYear();
            var listCourses = parser.GetCourses();
            var classService = new ClassService();
            var courseService = new CourseService();
            var reportService = new GradeReportService();
            classService.AddClass(myclass);
            foreach (var course in listCourses)
            {
                if (courseService.AddCourse(course) == null)
                {
                    continue;
                }

                Section section = AddSection(myclass, course.ID, term[0], year);

                var listStudents = classService.GetStudentsInClass(myclass);
                foreach (var student in listStudents)
                {
                    reportService.RegisterSection(section.ID, student.ID);
                }
            }
        }

        public Section AddSection(string classID, string courseID, char term, string year)
        {
            return repo.AddSection(classID, courseID, term, year);
        }

        public List<Student> GetSchedule(string classID, string courseID)
        {
            return repo.GetShedule(classID, courseID);
        }

        public List<Section> GetListSections()
        {
            return repo.GetListSections();
        }

        public Section GetSectionByClassCourse(string classID, string courseID)
        {
            return repo.GetSectionByClassCourse(classID, courseID);
        }

        public Section GetSection(int sectionID)
        {
            return repo.GetSection(sectionID);
        }
    }
}
