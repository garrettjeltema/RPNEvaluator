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
                // case 1: if token is an integer push onto stack
                if (int.TryParse(token, out int num))
                {
                    stack.Push(num);
                }

                // case 2: if token is a variable look in dictionary
                else if (vars != null && vars.ContainsKey(token))
                {
                    stack.Push(vars[token]);
                }

                // case 3: if token is an operator, compute
                else
                {
                    // pop the values from stack
                    int b = stack.Pop();
                    int a = stack.Pop();

                    switch (token)
                    { 
                        case "+": stack.Push(a + b); break; // addition case
                        case "-": stack.Push(a - b); break; // subtraction case
                        case "*": stack.Push(a * b); break; // multiplication case
                        case "/":                           // division case
                            if (b == 0)
                            {
                                throw new DivideByZeroException("Attempted to divide by zero");
                            }
                            stack.Push(a / b);
                            break;
                        case "%":                           // modulus case
                            if (b == 0)
                            {
                                throw new DivideByZeroException("Attempted to modulo by zero");
                            }
                            stack.Push(a % b);
                            break;
                        default: throw new Exception("Invalid token"); // error case
                    }
                }
            }

            // return the result which should be the only value left
            return stack.Pop();
        }
    }
}
