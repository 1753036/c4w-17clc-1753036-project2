using student_management.DataAccess;
using student_management.Models;
using student_management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.CsvLoaders
{
    public class CsvGradeReportParser : CsvParser
    {
        public CsvGradeReportParser(string filename)
            : base(filename)
        {
            if (data.Count() == 0)
            {
                return;
            }

            if (data[0].Count() != 7)
            {
                throw new CsvGradeReportWrongFormat("Csv file wrong format");
            }
        }

        public int GetNumGradeReport()
        {
            return data.Count() - 2;
        }

        public string GetClassName()
        {
            return data[0][0].Split('-')[0];
        }

        public string GetCourseID()
        {
            return data[0][0].Split('-')[1];
        }
        
        public List<GradeReport> GetListGradeReports()
        {
            var listGradeReports = new List<GradeReport>();
            var gradeReportRepo = new GradeReportRepo();
            var sectionRepo = new SectionRepo();
            var myclass = GetClassName();
            var mycourse = GetCourseID();
            var section = sectionRepo.GetSectionByClassCourse(myclass, mycourse);
            var num = GetNumGradeReport();

            if (section == null)
            {
                return listGradeReports;
            }

            for (int i = 0; i < num; ++i)
            {
                int index = i + 2;
                var report = new GradeReport();
                report.SectionID = section.ID;
                report.StudentID = data[index][1];
                report.Midterm = double.Parse(data[index][3]);
                report.Final = double.Parse(data[index][4]);
                report.Other = double.Parse(data[index][5]);
                report.Total = double.Parse(data[index][6]);
                listGradeReports.Add(report);
            }

            return listGradeReports;
        }
    }
}
