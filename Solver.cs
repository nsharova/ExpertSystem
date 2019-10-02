using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ExpertSystem
{
    public class Solver
    {
        public Solver(Rules rules, IDictionary<char, bool?> dict)
        {
            this.allrules = rules;
            this.dict = dict;
        }

        public Rules allrules { get; set; }
        public IDictionary<char, bool?> dict { get; set; }
       
        bool onlyAnd(string right)
        {
            if (Regex.Match(right, @"^[A-Z+!]+$").Success == false)
            {
                return false;
            }
            for (int i = 0; i < right.Length; i++)
            {
                if (right[i] == '!' && dict[right[i + 1]] == true)
                    return false;
                else if (right[i] == '!' && dict[right[i + 1]] == false)
                    i++;
                else if (char.IsUpper(right[i]) && dict[right[i]] == false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Recursion(char searchFact)
        {
            Stack<bool> variants = new Stack<bool>();
            foreach (Rule r in allrules.rules)
            {
                if (r.visited == false &&
                    ((r.right.Length == 1 && r.right[0] == searchFact) ||
                     (r.right.Length == 2 && r.right[0] == '!' && r.right[1] == searchFact)))
                {
                    bool fact = ToSolvePolishNotation(r);
                    fact = r.right[0] == '!' ? !fact : fact;
                    variants.Push(fact);
                }
            }

            if (variants.Count != 0)
            {
                return variants.Contains(true);
            }

            foreach (Rule r in allrules.rules)
            {
                if (r.visited == false &&
                    r.right.Length >= 3 &&
                    onlyAnd(r.right) &&
                    !r.left.Contains(searchFact.ToString()) &&
                    r.right.Contains(searchFact.ToString()) &&
                    ToSolvePolishNotation(r))
                {
                    for (int i = 0; i < r.right.Length; i++)
                    {
                        if (r.right[i] == '!')
                        {
                            dict[r.right[i + 1]] = false;
                            i++;
                        }
                        else if (char.IsUpper(r.right[i]))
                            dict[r.right[i]] = true;
                    }
                    if (dict[searchFact] != null)
                        return (bool)dict[searchFact];
                    return true;
                }
            }

            return false;
        }

        bool ToSolvePolishNotation(Rule rule)
        {

            Stack<Fact> stack = new Stack<Fact>();
            rule.visited = true;

            if (rule.left.Length == 1 || (rule.left.Length == 2 && rule.left[0] == '!'))
            {
                Fact fact;
                if (rule.left[0] == '!')
                    fact = new Fact(dict[rule.left[1]], rule.left[1], true);
                else
                    fact = new Fact(dict[rule.left[0]], rule.left[0]);
                if (fact.value == null)
                {
                    fact.value = Recursion(fact.name);
                    dict[fact.name] = fact.value;
                }
                return (bool)(fact.negative ? !fact.value : fact.value);
                
            }


            for (int i = 0; i < rule.left.Length; i++)
            {
                if (rule.left[i] == '!')
                {
                    stack.Push(new Fact(dict[rule.left[i + 1]], rule.left[i + 1], true));
                    i++;
                }
                else if (char.IsUpper(rule.left[i]))
                {
                    stack.Push(new Fact(dict[rule.left[i]], rule.left[i]));
                }
                else
                {
                    var s1 = stack.Pop();
                    var s2 = stack.Pop();
                    if (s1.value == null)
                    {
                        s1.value = Recursion(s1.name);
                        dict[s1.name] = s1.value;
                    }
                    if (s2.value == null)
                    {
                        s2.value = Recursion(s2.name);
                        dict[s2.name] = s2.value;
                    }
                    s1.value = s1.negative ? !s1.value : s1.value;
                    s2.value = s2.negative ? !s2.value : s2.value;
                    stack.Push(new Fact(MakeOperation((bool)s1.value, (bool)s2.value, rule.left[i])));
                }
            }
            if (stack.Count > 1)
            {
                throw new System.IO.InvalidDataException("The expression is not correct");
            }

            rule.visited = false;

            return (bool)stack.Pop().value;
        }

        bool MakeOperation(bool s1, bool s2, char operation)
        {
            if (operation == '+')
                return (s1 && s2);
            else if (operation == '|')
                return (s1 || s2);
            else if (operation == '^')
                return (s1 ^ s2);
            throw new System.InvalidOperationException("Unknown operation");
        }

    }
}
