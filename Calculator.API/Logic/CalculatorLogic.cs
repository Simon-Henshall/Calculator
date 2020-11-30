using System;

namespace Calculator.API.Logic
{
    public class CalculatorSymbols
    {
        public const string Plus = "+";
        public const string Minus = "-";
        public const string Times = "X";
        public const string Divide = "/";
    }

    public class CalculatorLogic
    {
        public static double Calculate(int operand1, string symbol, int operand2)
        {
            switch (symbol)
            {
                case CalculatorSymbols.Plus:
                    return operand1 + operand2;
                case CalculatorSymbols.Minus:
                    return operand1 - operand2;
                case CalculatorSymbols.Times:
                    return operand1 * operand2;
                case CalculatorSymbols.Divide:
                    return (double)operand1 / operand2;
                default:
                    throw new NotImplementedException($"{symbol} is not currently supported");
            }

        }
    }

    public class SymbolInvalidException : Exception
    {
    }
}