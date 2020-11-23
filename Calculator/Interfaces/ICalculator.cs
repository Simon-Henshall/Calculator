using Calculator.Models;
using System.Threading.Tasks;

namespace Calculator.Interfaces
{
    public interface ICalculator
    {
        Task<ResponseCalculationModel> Calculate(RequestCalculationModel request);
    }
}