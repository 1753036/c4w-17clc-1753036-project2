using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.CsvLoaders
{
    public class CsvClassWrongFormat : Exception
    {
        public CsvClassWrongFormat()
        {
        }

        public CsvClassWrongFormat(string message)
            : base(message)
        {
        }

        public CsvClassWrongFormat(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class CsvCourseWrongFormat : Exception
    {
        public CsvCourseWrongFormat()
        {
        }

        public CsvCourseWrongFormat(string message)
            : base(message)
        {
        }

        public CsvCourseWrongFormat(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class CsvGradeReportWrongFormat : Exception
    {
        public CsvGradeReportWrongFormat()
        {
        }

        public CsvGradeReportWrongFormat(string message)
            : base(message)
        {
        }

        public CsvGradeReportWrongFormat(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

