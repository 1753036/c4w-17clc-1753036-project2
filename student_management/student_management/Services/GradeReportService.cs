using student_management.DataAccess;
using student_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Services
{
    public class GradeReportService
    {
        GradeReportRepo repo = new GradeReportRepo();
        public GradeReport RegisterSection(int sectionID, string studentID)
        {
            return repo.RegisterSection(sectionID, studentID);
        }

        public void CancleSection(int sectionID, string studentID)
        {
            repo.CancleSection(sectionID, studentID);
        }

        public GradeReport GetGradeReport(int sectionID, string studentID)
        {
            return repo.GetGradeReport(sectionID, studentID);
        }

        public GradeReport UpdateGradeReport(int sectionID, string studentID, double mid, double fin, double other, double total)
        {
            return repo.UpdateGradeReport(sectionID, studentID, mid, fin, other, total);
        }

        public List<GradeReport> GetListGradeReports(string studentID)
        {
            return repo.GetListGradeReports(studentID);
        }
    }
}
