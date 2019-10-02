using System;
using System.Collections.Generic;

namespace ExpertSystem
{
    public class RPNUtils
    {
        public static string ToReversePolishNotation(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new System.InvalidOperationException("No data to reverse in polish notation");
                string polish = null;
                Stack<char> stack = new Stack<char>();
                for (int i = 0; i < str.Length; i++)
                {
                    if (char.IsUpper(str[i]))
                    {
                        //string.Concat(polish, str[i]);
                        polish += str[i];
                     }
                    else if (str[i] == '(')
                        stack.Push(str[i]);
                    else if (str[i] == ')')
                    {
                        while (stack.Count != 0 && stack.Peek() != '(')
                        {
                            polish += stack.Peek();
                            stack.Pop();
                        }
                        if (stack.Count == 0 || stack.Peek() != '(')
                            throw new System.InvalidOperationException("Not valid brackets");
                    stack.Pop();
                    }
                else if (str[i] == '!' && (i != str.Length-1) && (i + 1) < str.Length && char.IsUpper(str[i + 1]))
                        polish += str[i];
                else if (str[i] == '+'  && (i != str.Length-1)  && (i + 1) < str.Length && (char.IsUpper(str[i + 1]) || str[i + 1] == '(') || (i + 2 < str.Length && str[i + 1] == '!' && char.IsUpper(str[i + 2])))
                        stack.Push(str[i]);
                else if (str[i] == '|' && (i != str.Length-1) && (i + 1) < str.Length && (char.IsUpper(str[i + 1]) || str[i + 1] == '(') || (i + 2 < str.Length && str[i + 1] == '!' && char.IsUpper(str[i + 2])))
                    {
                        while(stack.Count != 0 && stack.Peek() == '+')
                        {
                            polish += stack.Peek();
                            stack.Pop();
                        }
                    stack.Push(str[i]);
                    }
                else if (str[i] == '^' && (i != str.Length-1) && (i + 1) < str.Length && (char.IsUpper(str[i + 1]) || str[i + 1] == '(') || (str[i + 2] < str.Length && str[i + 1] == '!' && char.IsUpper(str[i + 2])))
                    {
                    while (stack.Count != 0 && (stack.Peek() == '+' || stack.Peek() == '|'))
                        {
                            polish += stack.Peek();
                            stack.Pop();
                        }
                    stack.Push(str[i]);
                    }
                    else
                    {
                           throw new System.InvalidOperationException("not valid syntax in rule" + str);
                    }                           

            }
            if (stack.Count != 0)
            {
                while (stack.Count != 0 && stack.Peek() != '(')
                {
                    polish += stack.Peek();
                    stack.Pop();
                }
            }
            if (stack.Count != 0 && stack.Peek() == '(')
                throw new System.InvalidOperationException("Not valid brackets");

            return polish;
        }

    }
}