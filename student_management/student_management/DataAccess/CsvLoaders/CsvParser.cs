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
        protected List<string[]> data;
        public bool IsOpen = false;
        public CsvParser(string filename)
        {
            data = new List<string[]>();
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(@filename);
            }
            catch
            {
                return;
            }

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                data.Add(line.Split(','));
            }
            reader.Close();
            IsOpen = true;
        }

        public void Print()
        {
            foreach (var d in data)
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
