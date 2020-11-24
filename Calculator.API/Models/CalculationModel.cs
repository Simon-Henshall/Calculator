using System.Collections.Generic;

namespace Calculator.Models
{
    public class CalculationModel
    {
        public List<int> Operands { get; set; } // The numbers to calculate
        public List<Symbols> Symbols { get; set; } // The operator
    }
}