using System;
using System.Collections.Generic;

namespace RPNEvaluator
{
    public class RPNEvaluator
    {
        public static int Evaluate(string expr, Dictionary<string, int> vars)
        {
            // initialize stack
            var stack = new Stack<int>();

            // initialize input string into tokens
            var tokens = expr.Split(' ');

            foreach (var token in tokens)
            {
                if (int.TryParse(token, out int num))
                {
                    stack.Push(num);
                }
                else if (vars != null && vars.ContainsKey(token))
                {
                    stack.Push(vars[token]);
                }
            }
        }
    }
}
