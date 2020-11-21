using Calculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Controllers
{
    [ApiController]
    [Route("calculator")]
    public class CalculatorController : Controller
    {
        [HttpPost("calculate")]
        public ActionResult Calculate(RequestCalculationModel request)
        {
            double result = request.Symbol switch
            {
                "+" => request.Operand1 + request.Operand2,
                "-" => request.Operand1 - request.Operand2,
                "X" => request.Operand1 * request.Operand2,
                "/" => (double)request.Operand1 / request.Operand2, // cast required for int division
                _ => 0, // default
            };

            return Json(new ResponseCalculationModel { Result = result });
        }
    }
}
