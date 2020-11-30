using System.Collections.Generic;

namespace Calculator.API.Models
{
    public class CalculationModel
    {
        public List<int> Operands { get; set; } // The numbers to calculate
        public List<string> Symbols { get; set; } // The operator
    }
}