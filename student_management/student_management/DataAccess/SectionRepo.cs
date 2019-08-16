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
                listSections.Add(SectionReader.Read(ref rd));
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
                sec = SectionReader.Read(ref rd);
            }
            return sec;
        }

        public Section GetSection(int sectionID)
        {
            var cmd = dbconn.SqlCommand("SELECT * FROM section WHERE id=?", sectionID);
            var rd = cmd.ExecuteReader();
            Section sec = null;
            while (rd.Read())
            {
                sec = SectionReader.Read(ref rd);
            }
            return sec;
        }

        public List<Course> GetShedule(string classID)
        {
            var listCourses = new List<Course>();
            var cmd = dbconn.SqlCommand("SELECT c.* FROM section s JOIN course c ON s.course_id = c.id WHERE class_id=?", classID);
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                listCourses.Add(CourseReader.Read(ref rd));
            }
            return listCourses;
        }
    }
}
