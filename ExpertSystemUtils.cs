using System;
using System.Collections.Generic;

namespace ExpertSystem
{
    public class ExpertSystemUtils
    {
        public static void PushFactToDictionary(Rules newrules, IDictionary<char, bool?> exSystem)
        {
            foreach (Rule r in newrules.rules)
            {
                GetFactsToDict(r.left, exSystem, newrules);
                GetFactsToDict(r.right,exSystem, newrules);
                GetFactsToDict(newrules.queries, exSystem, newrules);
                GetInitToDict(newrules, exSystem);
            }
        }

        public static void GetFactsToDict(string str, IDictionary<char, bool?> exSystem,Rules newrules)
        {
            for (int i = 0; i < str.Length; i++)
            {
                bool? value = (newrules.initials.Contains(Char.ToString(str[i]))) ? true : (bool?)null;
                if (char.IsUpper(str[i]) && !exSystem.ContainsKey(str[i]))
                {
                    exSystem.Add(str[i], value);
                }
            }
        }

        public static void GetInitToDict(Rules newrules, IDictionary<char, bool?> exSystem)
        {
            for (int i = 0; i < newrules.initials.Length; i++)
            {
                if (char.IsUpper(newrules.initials[i]) && !exSystem.ContainsKey(newrules.initials[i]))
                {
                    exSystem.Add(newrules.initials[i], true);
                }
            }
        }
    }
}

