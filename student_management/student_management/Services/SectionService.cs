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
    }
}
