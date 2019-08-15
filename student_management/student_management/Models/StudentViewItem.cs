using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Models
{
    public class StudentViewItem
    {
        public StudentViewItem(Student student)
        {
            ID = student.ID;
            Fullname = student.Fullname;
            Birthday = student.Birthday;
            SocialID = student.SocialID;
            switch (student.Gender)
            {
                case 'M': Gender = "Nam"; break;
                case 'F': Gender = "Nữ"; break;
                default: Gender = "Khác"; break;
            }
        }
        public string ID { get; set; }
        public string Fullname { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string SocialID { get; set; }
    }
}
