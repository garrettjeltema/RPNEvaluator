using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
                else if (vars.ContainsKey(token))
                {
                    stack.Push(vars[token]);
                }

                // case 3: if token is an operator, compute
                else if (IsOperator(token))
                {
                    // if not enough tokens error case
                    if (stack.Count < 2)
                    {
                        throw new InvalidOperationException("Not enough tokens");
                    }
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
                        default: throw new Exception("Invalid token"); // invalid operator error case
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid token");
                }
            }
            
            // if not only one result remains
            if (stack.Count != 1)
            {
                throw new InvalidOperationException("Invalid result");
            }

            // return the result which should be the only value left
            return stack.Pop();
        }

        public static float Evaluatef(string expr, Dictionary<string, float> vars)
        {
            // initialize stack
            var stack = new Stack<float>();

            // initialize input string into tokens
            var tokens = expr.Split(' ');

            foreach (var token in tokens)
            {
                // case 1: if token is a float push onto stack
                if (float.TryParse(token, out float num))
                {
                    stack.Push(num);
                }

                // case 2: if token is a variable look in dictionary
                else if (vars.ContainsKey(token))
                {
                    stack.Push(vars[token]);
                }

                // case 3: if token is an operator, compute
                else if (IsOperator(token))
                {
                    // if not enough tokens error case
                    if (stack.Count < 2)
                    {
                        throw new InvalidOperationException("Not enough tokens");
                    }

                    float b = stack.Pop();
                    float a = stack.Pop();

                    switch (token)
                    {
                        case "+": stack.Push(a + b); break; // addition case
                        case "-": stack.Push(a - b); break; // subtraction case
                        case "*": stack.Push(a * b); break; // multiplication case
                        case "/":                           // division case
                            if (b == 0.0f)
                            {
                                throw new DivideByZeroException("Attempted to divide by zero");
                            }
                            stack.Push(a / b);
                            break;
                        case "%":                           // modulus case
                            if (b == 0.0f)
                            {
                                throw new DivideByZeroException("Attempted to modulo by zero");
                            }
                            stack.Push(a % b);
                            break;
                        default:
                            throw new Exception("Invalid token"); // invalid operator error case
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid token");
                }
            }

            if (stack.Count != 1)
            {
                throw new InvalidOperationException("Invalid result");
            }
            // return the result which should be the only value left
            return stack.Pop();
        }

        public static float Evaluatef(string expr, Dictionary<string, int> vars)
        {
            // convert integer dictionary into float dictionary
            var floatVars = new Dictionary<string, float>();

            foreach (var kv in vars)
            {
                floatVars[kv.Key] = kv.Value; // convert ints to floats
            }

            // return the float evaluation method
            return Evaluatef(expr, floatVars);
        }

        // private method used in order to check if a token is an operator
        private static bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/" || token == "%";
        }
    }
}
