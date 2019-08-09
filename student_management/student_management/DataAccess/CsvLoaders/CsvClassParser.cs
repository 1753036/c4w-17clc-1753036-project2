using student_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.CsvLoaders
{
    public class CsvClassParser : CsvParser
    {
        public CsvClassParser(string filename)
            : base(filename)
        {
            if (Data[0].Count() != 6)
            {
                throw new CsvClassWrongFormat("Csv file wrong format");
            }
        }

        public string GetClassName()
        {
            return Data[0][0];
        }

        public int GetNumStudent()
        {
            return Data.Count() - 2;
        }

        private char GetCharGender(string gender)
        {
            if (gender.Count() == 3)
            {
                return 'M';
            }
            else if (gender.Count() == 2)
            {
                return 'F';
            }
            return 'E';
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            int num = GetNumStudent();

            for (int i = 0; i < num; ++i)
            {
                int index = i + 2;
                var student = new Student();
                student.ClassID = GetClassName();
                student.ID = Data[index][1];
                student.Fullname = Data[index][2];
                student.Gender = GetCharGender(Data[index][3]);
                student.Birthday = Data[index][4];
                student.SocialID = Data[index][5];
                students.Add(student);
            }

            return students;
        }
    }
}
