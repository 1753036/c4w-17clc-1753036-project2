using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Models
{
    public class Section
    {
        public int ID { get; set; }
        public string ClassID { get; set; }
        public string CourseID { get; set; }
        public char Term { get; set; }
        public string AcademicYear { get; set; }
    }
}
