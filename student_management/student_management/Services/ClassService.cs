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
        public void AddClass(string id)
        {
            repo.AddClass(id);
        }

        public bool AddStudent(Student student)
        {
            if (repo.IsClassExist(student.ClassID) == false)
            {
                repo.AddClass(student.ClassID);
            }

            if (GetStudent(student.ID) == null)
            {
                repo.AddStudent(student);
                return true;
            }

            return false;
        }

        public Student GetStudent(string id)
        {
            return repo.GetStudentByID(id);
        }

        public List<Class> GetListClasses()
        {
            return repo.GetListClasses();
        }
    }
}
