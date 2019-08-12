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

        public void RegisterSection(int sectionID, string studentID)
        {
            repo.RegisterSection(sectionID, studentID);
        }

        public List<Section> GetListSections()
        {
            return repo.GetListSections();
        }
    }
}
