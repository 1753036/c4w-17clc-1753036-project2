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
        public float Midterm { get; set; }
        public float Final { get; set; }
        public float Other { get; set; }
        public float Total { get; set; }
    }
}
