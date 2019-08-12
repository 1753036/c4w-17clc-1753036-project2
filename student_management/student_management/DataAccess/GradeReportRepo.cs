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
        public GradeReport AddGradeReport(int sectionID, string studentID, float mid, float fin, float other, float total)
        {
            OleDbCommand cmd = dbconn.SqlCommand(
                "INSERT INTO grade_report VALUES(?, ?, ?, ?, ?, ?)",
                sectionID, studentID, mid, fin, other, total
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
                gradeReport.SectionID = sectionID;
                gradeReport.StudentID = studentID;
                gradeReport.Midterm = rd.GetFloat(2);
                gradeReport.Final = rd.GetFloat(3);
                gradeReport.Other = rd.GetFloat(4);
                gradeReport.Total = rd.GetFloat(5);
            }

            return gradeReport;
        }

        public GradeReport UpdateGradeReport(int sectionID, string studentID, float mid, float fin, float other, float total)
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
