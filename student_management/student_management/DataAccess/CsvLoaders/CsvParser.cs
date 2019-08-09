using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management
{
    public class CsvParser
    {
        public List<string[]> Data;
        public CsvParser(string filename)
        {
            Data = new List<string[]>();
            StreamReader reader = new StreamReader(@filename);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                Data.Add(line.Split(','));
            }
            reader.Close();
        }

        public void Print()
        {
            foreach (var d in Data)
            {
                foreach (var s in d)
                {
                    Console.Write("{0} ", s);
                }
                Console.WriteLine();
            }
        }
    }
}
