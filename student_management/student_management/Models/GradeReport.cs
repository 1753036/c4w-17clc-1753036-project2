using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Models
{
    public class GradeReport
    {
        public int SectionID { get; set; }
        public string StudentID { get; set; }
        public double Midterm { get; set; }
        public double Final { get; set; }
        public double Other { get; set; }
        public double Total { get; set; }

        public void Print()
        {
            Console.WriteLine("section: {0}, student {1}, {2}, {3}, {4}, {5}", SectionID, StudentID, Midterm, Final, Other, Total);
        }
    }
}
