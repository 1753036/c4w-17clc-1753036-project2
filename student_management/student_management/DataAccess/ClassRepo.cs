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
        private DbConnection dbconn = DbConnection.Instance();
        public bool IsClassExist(string classID)
        {
            OleDbCommand cmd = dbconn.SqlCommand("SELECT * FROM class WHERE id=?", classID);
            OleDbDataReader rd = cmd.ExecuteReader();
            string classId = null;
            while(rd.Read())
            {
                classId = rd.GetString(0);
            }
            return classId != null;
        }

        public Class AddClass(string classID)
        {
            OleDbCommand cmd = dbconn.SqlCommand("INSERT INTO class VALUES(?)", classID);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return null;
            }

            Class classs = new Class();
            classs.ID = classID;
            classs.MemberCount = 0 ;
            return classs;
        }

        public Student AddStudent(string studentID, string fullname, char gender, string birthday, string socialID, string classID)
        {
            OleDbCommand cmd = dbconn.SqlCommand(
                "EXEC dbo.sp_add_student ?, ?, ?, ?, ?, ?",
                studentID, fullname, gender, birthday, socialID, classID
            );

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return null;
            }

            Student student = new Student();
            student.ID = studentID;
            student.Fullname = fullname;
            student.Gender = gender;
            student.Birthday = birthday;
            student.SocialID = socialID;
            student.ClassID = classID;

            return student;
        }

        public Student GetStudentByID(string studentID)
        {
            OleDbCommand cmd = dbconn.SqlCommand("SELECT * FROM student WHERE id=?", studentID);
            OleDbDataReader rd = cmd.ExecuteReader();
            Student student = null;

            while (rd.Read())
            {
                student = StudentReader.Read(ref rd);
            }

            return student;
        }

        public List<Class> GetListClasses()
        {
            List<Class> listClasses = new List<Class>();
            OleDbCommand cmd = dbconn.SqlCommand("SELECT * FROM class");
            OleDbDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Class cl = new Class();
                cl.ID = rd.GetString(0);
                cl.MemberCount = GetStudentsInClass(cl.ID).Count();
                listClasses.Add(cl);
            }

            return listClasses;
        }

        public List<Student> GetStudentsInClass(string classID)
        {
            List<Student> listStudents = new List<Student>();
            OleDbCommand cmd = dbconn.SqlCommand("SELECT * FROM student WHERE class_id=?", classID);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                listStudents.Add(StudentReader.Read(ref rd));
            }
            return listStudents;
        }
    }
}
