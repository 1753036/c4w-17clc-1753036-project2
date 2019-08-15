using student_management.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.DataAccess
{
    public class SectionReader
    {
        public static Section Read(ref OleDbDataReader rd)
        {
            var sec = new Section();
            sec.ID = rd.GetInt32(0);
            sec.ClassID = rd.GetString(1);
            sec.CourseID = rd.GetString(2);
            sec.Term = rd.GetString(3)[0];
            sec.AcademicYear = rd.GetString(4);
            return sec;
        }
    }
}
