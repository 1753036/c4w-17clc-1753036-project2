using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using student_management.Models;

namespace student_management.DataAccess
{
    public class GradeReportRepo
    {
        DbConnection dbconn = DbConnection.Instance();
        public GradeReport RegisterSection(int sectionID, string studentID)
        {
            OleDbCommand cmd = dbconn.SqlCommand(
                "EXEC sp_register_section ?, ?",
                sectionID, studentID
            );

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return null;
            }

            return GetGradeReport(sectionID, studentID);
        }

        public void CancleSection(int sectionID, string studentID)
        {
            OleDbCommand cmd = dbconn.SqlCommand(
                "EXEC sp_cancle_section ?, ?",
                sectionID, studentID
            );

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("ERROR");
            }
        }

        public GradeReport GetGradeReport(int sectionID, string studentID)
        {
            OleDbCommand cmd = dbconn.SqlCommand(
                "SELECT * FROM grade_report WHERE section_id=? AND student_id=?",
                sectionID, studentID
            );
            OleDbDataReader rd = cmd.ExecuteReader();
            GradeReport gradeReport = null;

            while (rd.Read())
            {
                gradeReport = GradeReportReader.Read(ref rd);
            }

            return gradeReport;
        }

        public GradeReport UpdateGradeReport(int sectionID, string studentID, double mid, double fin, double other, double total)
        {
            OleDbCommand cmd = dbconn.SqlCommand(
                "UPDATE grade_report SET midterm=?, final=?, other=?, total=? WHERE section_id=? AND student_id=?",
                mid, fin, other, total, sectionID, studentID
            );

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return null;
            }

            GradeReport gradeReport = new GradeReport();
            gradeReport.SectionID = sectionID;
            gradeReport.StudentID = studentID;
            gradeReport.Midterm = mid;
            gradeReport.Final = fin;
            gradeReport.Other = other;
            gradeReport.Total = total;
            return gradeReport;
        }

        public List<GradeReport> GetListGradeReports(string studentID)
        {
            List<GradeReport> listGradeReports = new List<GradeReport>();
            OleDbCommand cmd = dbconn.SqlCommand(
                "SELECT * FROM grade_report WHERE student_id=?",
                studentID);

            OleDbDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                listGradeReports.Add(GradeReportReader.Read(ref rd));
            }
            return listGradeReports;
        }

        public List<GradeReport> GetListGradeReportBySection(int sectionID)
        {
            List<GradeReport> listGradeReports = new List<GradeReport>();
            OleDbCommand cmd = dbconn.SqlCommand("SELECT * FROM grade_report WHERE section_id=?", sectionID);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                listGradeReports.Add(GradeReportReader.Read(ref rd));
            }
            return listGradeReports;
        }
    }
}
