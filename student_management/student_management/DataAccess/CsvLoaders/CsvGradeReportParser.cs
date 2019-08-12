using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.CsvLoaders
{
    public class CsvGradeReportParser : CsvParser
    {
        public CsvGradeReportParser(string filename)
            : base(filename)
        {
            if (data[0].Count() != 7)
            {
                throw new CsvClassWrongFormat("Csv file wrong format");
            }
        }
    }
}
