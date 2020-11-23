using Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.CompilerServices;
using System.Web.Http;

namespace Calculator.Controllers
{
    [ApiController]
    [ValidateModel]
    [Microsoft.AspNetCore.Mvc.Route("calculator")]
    public class CalculatorController : Controller
    {
        [Microsoft.AspNetCore.Mvc.HttpPost("calculate")]
        public ActionResult Calculate(RequestCalculationModel request)
        {
            try
            {
#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
                double result = request.Symbol switch
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
                {
                    "+" => request.Operand1 + request.Operand2,
                    "-" => request.Operand1 - request.Operand2,
                    "X" => request.Operand1 * request.Operand2,
                    "/" => (double)request.Operand1 / request.Operand2 // cast required for int division
                };

                return Json(new ResponseCalculationModel { Result = result });
            }
            catch (SwitchExpressionException ex)
            {
                // ToDo: Log error
                return BadRequest($"Invalid symbol: {ex.UnmatchedValue}");
            }
            catch (Exception ex)
            {
                // ToDo: Log error
                return BadRequest($"Unknown error: {ex}");
            }
        }
    }
}