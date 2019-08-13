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
                gradeReport = new GradeReport();
                gradeReport.SectionID = rd.GetInt32(0);
                gradeReport.StudentID = rd.GetString(1);
                gradeReport.Midterm = rd.GetDouble(2);
                gradeReport.Final = rd.GetDouble(3);
                gradeReport.Other = rd.GetDouble(4);
                gradeReport.Total = rd.GetDouble(5);
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
    }
}
