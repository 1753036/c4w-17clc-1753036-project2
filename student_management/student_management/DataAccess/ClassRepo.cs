using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using student_management.Models;

namespace student_management.DataAccess
{
    public class ClassRepo
    {
        public bool IsClassExist(string id)
        {
            string class_id = null;
            Database.Open();
            OleDbCommand cmd = Database.Command("SELECT * FROM class WHERE id=?");
            cmd.Parameters.Add(new OleDbParameter("id", id));
            var rd = cmd.ExecuteReader();
            while(rd.Read())
            {
                class_id = rd.GetString(0);
            }
            return class_id != null;
        }

        public void AddClass(string id)
        {
            var conn = Database.Open();
            OleDbCommand cmd = Database.Command("INSERT INTO class VALUES(?)");
            cmd.Parameters.Add(new OleDbParameter("classid", id));
            cmd.ExecuteNonQuery();
        }

        public void AddStudent(Student student)
        {
            Database.Open();
            OleDbCommand cmd = Database.Command("EXEC dbo.sp_add_student ?, ?, ?, ?, ?");
            cmd.Parameters.Add(new OleDbParameter("id", student.ID));
            cmd.Parameters.Add(new OleDbParameter("fullname", student.Fullname));
            cmd.Parameters.Add(new OleDbParameter("gender", student.Gender));
            cmd.Parameters.Add(new OleDbParameter("social_id", student.SocialID));
            cmd.Parameters.Add(new OleDbParameter("class_id", student.ClassID));
            cmd.ExecuteNonQuery();
        }

        public Student GetStudentByID(string id)
        {
            Student newStudent = null;
            var conn = Database.Open();
            OleDbCommand cmd = Database.Command("SELECT * FROM student WHERE id=?");
            cmd.Parameters.Add(new OleDbParameter("id", id));
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                newStudent = new Student();
                newStudent.ID = rd.GetString(0);
                newStudent.Fullname = rd.GetString(1);
                newStudent.Gender = rd.GetString(2)[0];
                newStudent.SocialID = rd.GetString(3);
                newStudent.ClassID = rd.GetString(4);
            }
            return newStudent;
        }

        public List<Class> GetListClasses()
        {
            List<Class> classes = new List<Class>();
            OleDbCommand cmd = Database.Command("SELECT * FROM class");
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Class cl = new Class();
                cl.ID = rd.GetString(0);
                cl.MemberCount = GetStudentsInClass(cl.ID).Count();
                classes.Add(cl);
            }
            return classes;
        }

        public List<Student> GetStudentsInClass(string id)
        {
            List<Student> students = new List<Student>();
            OleDbCommand cmd = Database.Command("SELECT * FROM student WHERE class_id=?");
            cmd.Parameters.Add(new OleDbParameter("class_id", id));
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Student st = new Student();
                st.ID = rd.GetString(0);
                st.Fullname = rd.GetString(1);
                st.Gender = rd.GetString(2)[0];
                st.Birthday = rd.GetString(3);
                st.SocialID = rd.GetString(4);
                st.ClassID = rd.GetString(5);
                students.Add(st);
            }
            return students;
        }
    }
}
