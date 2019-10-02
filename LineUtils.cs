using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
{
    class LineUtils
    {
        public static void ValidateEmptiness(List<string> lines)
        {
            if (lines == null || lines.Count == 0)
            {
                throw new Exception("File is empty.");
            }
        }

        public static List<string> FormatLines(List<string> lines)
        {
            List<string> formattedLines = new List<string>();
            foreach (string line in lines)
            {
                if (line != null && !string.IsNullOrEmpty(line.Trim()))
                {
                    string str = line;
                    if (line.Contains("#"))
                        str = line.Substring(0, str.IndexOf("#"));
                    if (!string.IsNullOrEmpty(str))
                        formattedLines.Add(str);
                }
            }
            if (formattedLines.Count == 0)
                throw new Exception("File is empty or has only comments.");
            return formattedLines;
        }
    }
}
