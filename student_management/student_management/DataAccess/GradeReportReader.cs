using student_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace student_management.DataAccess
{
    public class GradeReportReader
    {
        public static GradeReport Read(ref OleDbDataReader rd)
        {
            var gradeReport = new GradeReport();
            gradeReport.SectionID = rd.GetInt32(0);
            gradeReport.StudentID = rd.GetString(1);
            gradeReport.Midterm = rd.GetDouble(2);
            gradeReport.Final = rd.GetDouble(3);
            gradeReport.Other = rd.GetDouble(4);
            gradeReport.Total = rd.GetDouble(5);
            return gradeReport;
        }
    }
}
