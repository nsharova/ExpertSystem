using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;


namespace ExpertSystem
{
    public class ValidateDataUtils
    {
        public static void ParseDataFromFile(List <string> lines, Rules newrules)
        {
            if (lines.Count <= 2)
                throw new System.InvalidOperationException("Not enought data");
            string queries = lines.Last();
            string initials = lines[lines.Count - 2];
            if (ValidateInitials(initials.Replace(" ", "")))
            {
                newrules.initials = (initials.Substring(initials.LastIndexOf('=') + 1)).Replace(" ", "");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(initials);
            }
            if (ValidateQueries(queries.Replace(" ", "")))
            {
                newrules.queries = (queries.Substring(queries.LastIndexOf('?') + 1)).Replace(" ", "");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(queries);
            }
            
            for (int i = 0; i < lines.Count-2; i++)
            {
                if (ValidateRules(lines[i]))
                {
                    lines[i] = (Regex.Replace(lines[i], @"\s+", ""));
                    ParseAndAddRules(lines[i], newrules);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(lines[i]);
                }

            }

        }

        public static bool ValidateInitials(string str)
        {
            var regex = @"^=[A-Z]*$";
            Match match = Regex.Match(str, regex);
            if (match.Success)
                return true;
            else
            {
                throw new System.InvalidOperationException("Not valid initials line");
            }
        }

        public static bool ValidateQueries(string str)
        {
            var regex = @"^\?[A-Z]+$";
            Match match = Regex.Match(str, regex);
            if (str != string.Empty && match.Success)
                return true;
            else
            {
                throw new System.InvalidOperationException("Not valid queries line");
            }
        }

        public static bool ValidateRules(string str)
        {
            var regex = @"[A-Z=<>|+!\^]";
            Match match = Regex.Match(str, regex);
            if (str != string.Empty && match.Success)
                return true;
            else
            {
                throw new System.InvalidOperationException("Incorrect rule: " + str);
            }
        }

        public static void ParseAndAddRules(string line, Rules newrules)
        {
            if (line.Contains("<=>"))
            {
                string[] side = line.Split(new string[] { "<=>" }, StringSplitOptions.RemoveEmptyEntries);
                if (side.Length == 2)
                {
                    Rule ruleFirst = new Rule();
                    ruleFirst.left = RPNUtils.ToReversePolishNotation(side[0]);
                    ruleFirst.right = RPNUtils.ToReversePolishNotation(side[1]);
                    ruleFirst.visited = false;
                    newrules.rules.Add(ruleFirst);
                    Rule ruleSecond = new Rule();
                    ruleSecond.left = RPNUtils.ToReversePolishNotation(side[1]);
                    ruleSecond.right = RPNUtils.ToReversePolishNotation(side[0]);
                    ruleSecond.visited = false;
                    newrules.rules.Add(ruleSecond);
                }
                else
                    throw new System.InvalidOperationException("Incorrect rule: " + line);
            }
            else if (line.Contains("=>"))
            {
                string[] side2 = line.Split(new string[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
                if (side2.Length == 2)
                {
                    Rule ruleOne = new Rule();
                    ruleOne.left = RPNUtils.ToReversePolishNotation(side2[0]);
                    ruleOne.right = RPNUtils.ToReversePolishNotation(side2[1]);
                    ruleOne.visited = false;
                    newrules.rules.Add(ruleOne);
                }
                else
                    throw new System.InvalidOperationException("Incorrect rule: " + line);;
            }
            else
                throw new System.InvalidOperationException("Incorrect rule: " + line);
        }

    }
}
