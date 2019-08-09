using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using student_management.Models;

namespace student_management.DataAccess
{
    public class SectionRepo
    {
        public void AddSection(ref Section section)
        {
            Database.Open();
            var cmd = Database.Command("INSERT INTO section VALUES (?, ?, ?, ?); SELECT SCOPE_IDENTITY()");
            cmd.Parameters.Add(new OleDbParameter("class", section.ClassID));
            cmd.Parameters.Add(new OleDbParameter("course_id", section.CourseID));
            cmd.Parameters.Add(new OleDbParameter("term", section.Term));
            cmd.Parameters.Add(new OleDbParameter("year", section.AcademicYear));
            section.ID = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void RegisterSection(Section section, string student_id)
        {
            Database.Open();
            var cmd = Database.Command("EXEC dbo.sp_register_section ?,?");
            cmd.Parameters.Add(new OleDbParameter("id", section.ID));
            cmd.Parameters.Add(new OleDbParameter("student_id", student_id));
            cmd.ExecuteNonQuery();
        }

        public List<Section> GetListSections()
        {
            List<Section> sections = new List<Section>();
            Database.Open();
            var cmd = Database.Command("SELECT * FROM section");
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Section sec = new Section();
                sec.ID = rd.GetInt32(0);
                sec.ClassID = rd.GetString(1);
                sec.CourseID = rd.GetString(2);
                sec.Term = rd.GetString(3)[0];
                sec.AcademicYear = rd.GetString(4);
                sections.Add(sec);
            }
            return sections;
        }
    }
}
