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
    public class GradeReportService
    {
        GradeReportRepo repo = new GradeReportRepo();

        public void ReadFromCsv(string filename)
        {
            CsvGradeReportParser parser = new CsvGradeReportParser(filename);
            var listReports = parser.GetListGradeReports();
            foreach (var report in listReports)
            {
                UpdateGradeReport(report);
            }
        }

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

        public GradeReport UpdateGradeReport(GradeReport r)
        {
            return UpdateGradeReport(r.SectionID, r.StudentID, r.Midterm, r.Final, r.Other, r.Total);
        }

        public List<GradeReport> GetListAllGradeReports()
        {
            return repo.GetListAllGradeReports();
        }

        public List<GradeReport> GetListGradeReports(string studentID)
        {
            return repo.GetListGradeReports(studentID);
        }

        public List<GradeReport> GetListGradeReportBySection(int sectionID)
        {
            return repo.GetListGradeReportBySection(sectionID);
        }
    }
}
