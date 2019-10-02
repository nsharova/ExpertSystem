using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
{
    class Program
    {
      
        static void Main(string[] args)
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + @"/" + args[0];

                if (args.Length != 1 || args[0] == null || string.IsNullOrEmpty(args[0].Trim()))
                {
                    throw new System.InvalidOperationException("Path to file is not specified!");
                }
                List<string> lines = IOUtils.ReadLinesFromFile(path);
                LineUtils.ValidateEmptiness(lines);
                lines = LineUtils.FormatLines(lines);

                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}
