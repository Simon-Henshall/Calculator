using Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Calculator.Interfaces
{
    public interface ICalculator
    {
        Task<IActionResult> Calculate(RequestCalculationModel request);
    }
}