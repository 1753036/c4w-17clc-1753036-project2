using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Models
{
    class Grade
    {
        string ClassID { get; set; }
        string CourseID { get; set; }
        string StudentID { get; set; }
        float Midterm { get; set; }
        float Final { get; set; }
        float Other { get; set; }
        float Total { get; set; }
    }
}
