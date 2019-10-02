using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ExpertSystem
{
    class Program
    {
      
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 1 || args[0] == null || string.IsNullOrEmpty(args[0].Trim()))
                {
                    throw new System.InvalidOperationException("Path to file is not specified!");
                }
                string path = args[0];
                Rules newrules = new Rules();
                newrules.rules = new List<Rule>();
                IDictionary<char, bool?> exSystem = new Dictionary<char, bool?>();

                List<string> lines = IOUtils.ReadLinesFromFile(path);
                LineUtils.ValidateEmptiness(lines);
                lines = LineUtils.FormatLines(lines);
                ValidateDataUtils.ParseDataFromFile(lines, newrules);
                ExpertSystemUtils.PushFactToDictionary(newrules, exSystem);

                Solve(newrules, exSystem);

                string line;
                while ((line = Console.ReadLine()) != null)
                {
                    if (Regex.Match(line, @"^=[A-Z]*$").Success || Regex.Match(line, @"^\?[A-Z]+$").Success)
                    {
                        if (Regex.Match(line, @"^=[A-Z]*$").Success)
                        {
                            newrules.initials = null;
                            newrules.initials += (line.Substring(line.LastIndexOf('=') + 1)).Replace(" ", "");
                            exSystem = new Dictionary<char, bool?>();
                            ExpertSystemUtils.PushFactToDictionary(newrules, exSystem);
                            Solve(newrules, exSystem);

                        }
                        else if (Regex.Match(line, @"^\?[A-Z]+$").Success)
                        {
                            newrules.queries = null;
                            newrules.queries += (line.Substring(line.LastIndexOf('?') + 1)).Replace(" ", "");
                            exSystem = new Dictionary<char, bool?>();
                            ExpertSystemUtils.PushFactToDictionary(newrules, exSystem);
                            Solve(newrules, exSystem);
                        }
                    }
                    else if (line != "exit")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Try again");
                    }
                    if (line == "exit")
                    {
                          break;
                    }

                 }
            } 
            catch(Exception e) 
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Not valid data! "+ e.Message);
            }

        }

        static void Solve(Rules newrules, IDictionary<char, bool?> exSystem)
        {
            Solver solver = new Solver(newrules, exSystem);

            for (int i = 0; i < newrules.queries.Length; i++)
            {
                if (solver.dict[newrules.queries[i]] == null)
                {
                    solver.dict[newrules.queries[i]] = solver.Recursion(newrules.queries[i]);
                }
                if (solver.dict[newrules.queries[i]] == true)
                {

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else if (solver.dict[newrules.queries[i]] == false)
                {

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                Console.WriteLine(newrules.queries[i] + " = " + solver.dict[newrules.queries[i]]);
            }

        }

    }
}
