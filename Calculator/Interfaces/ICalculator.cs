using Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Calculator.Interfaces
{
    public interface ICalculator
    {
        Task<ResponseCalculationModel> Calculate(RequestCalculationModel request);
    }
}