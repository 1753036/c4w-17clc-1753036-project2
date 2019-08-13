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

        public Section GetSectionByClassCourse(string classID, string courseID)
        {
            var cmd = dbconn.SqlCommand("SELECT * FROM section WHERE class_id=? AND course_id=?", classID, courseID);
            var rd = cmd.ExecuteReader();
            Section sec = null;
            while (rd.Read())
            {
                sec = new Section();
                sec.ID = rd.GetInt32(0);
                sec.ClassID = rd.GetString(1);
                sec.CourseID = rd.GetString(2);
                sec.Term = rd.GetString(3)[0];
                sec.AcademicYear = rd.GetString(4);
            }
            return sec;
        }

        public List<Student> GetShedule(string classID, string courseID)
        {
            List<Student> listStudents = new List<Student>();
            var cmd = dbconn.SqlCommand(
                "SELECT st.* FROM section s JOIN grade_report g ON s.id = g.section_id JOIN student st ON g.student_id = st.id WHERE s.class_id=? AND s.course_id=?",
                classID, courseID);
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                listStudents.Add(StudentReader.Read(ref rd));
            }

            return listStudents;
        }
    }
}
