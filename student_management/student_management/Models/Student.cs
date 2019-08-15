using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Models
{
    public class Student
    {
        public string ID { get; set; }
        public string Fullname { get; set; }
        public char Gender { get; set; }
        public string Birthday { get; set; }
        public string SocialID { get; set; }
        public string ClassID { get; set; }

        public void Print()
        {
            Console.WriteLine("{0}. {1} {2} {3} {4} {5}", ID,Fullname, Gender, Birthday, SocialID, ClassID);
        }
    }
}
