namespace Calculator.API.Models
{
    public class RequestCalculationModel
    {
        public int Operand1 { get; set; } // The first number to calculate
        public string Symbol { get; set; } // The operator
        public int Operand2 { get; set; } // The second number to calculate
    }
}