using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using student_management.Models;
using System.Windows;

namespace student_management.DataAccess
{
    public class SectionRepo
    {
        DbConnection dbconn = DbConnection.Instance();
        public Section AddSection(string classID, string courseID, char term, string year)
        {
            var cmd = dbconn.SqlCommand(
                "INSERT INTO section VALUES (?, ?, ?, ?); SELECT SCOPE_IDENTITY()",
                classID, courseID, term, year
            );
            int sectionID = -1;
            try
            {
                sectionID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                return null;
            }

            if (sectionID == -1)
            {
                return null;
            }

            Section section = new Section();
            section.ID = sectionID;
            section.ClassID = classID;
            section.CourseID = courseID;
            section.Term = term;
            section.AcademicYear = year;

            return section;
        }

        public void RegisterSection(int sectionID, string studentID)
        {
            var cmd = dbconn.SqlCommand("EXEC dbo.sp_register_section ?,?", sectionID, studentID);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                
            }
        }

        public List<Section> GetListSections()
        {
            List<Section> listSections = new List<Section>();
            var cmd = dbconn.SqlCommand("SELECT * FROM section");
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Section sec = new Section();
                sec.ID = rd.GetInt32(0);
                sec.ClassID = rd.GetString(1);
                sec.CourseID = rd.GetString(2);
                sec.Term = rd.GetString(3)[0];
                sec.AcademicYear = rd.GetString(4);
                listSections.Add(sec);
            }
            return listSections;
        }
    }
}
