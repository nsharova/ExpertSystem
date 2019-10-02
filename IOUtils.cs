using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ExpertSystem
{
    class IOUtils
    {
        public static List<string> ReadLinesFromFile(string path)
        {
            StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
            var array = new List<string>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                array.Add(line);
            }
            return array;
        }
    }
}
