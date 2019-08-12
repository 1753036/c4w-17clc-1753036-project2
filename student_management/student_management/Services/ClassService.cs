using student_management.DataAccess;
using student_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Services
{
    public class ClassService
    {
        ClassRepo repo = new ClassRepo();

        public Class AddClass(string classID)
        {
            return repo.AddClass(classID);
        }

        public Student AddStudent(string studentID, string fullname, char gender, string birthday, string socialID, string classID)
        {
            if (repo.IsClassExist(classID) == false)
            {
                repo.AddClass(classID);
            }

            return repo.AddStudent(studentID, fullname, gender, birthday, socialID, classID);
        }

        public Student AddStudent(Student student)
        {
            return this.AddStudent(student.ID, student.Fullname, student.Gender, student.Birthday, student.SocialID, student.ClassID);
        }

        public Student GetStudent(string studentID)
        {
            return repo.GetStudentByID(studentID);
        }

        public List<Class> GetListClasses()
        {
            return repo.GetListClasses();
        }

        public List<Student> GetStudentsInClass(string classID)
        {
            return repo.GetStudentsInClass(classID);
        }
    }
}
