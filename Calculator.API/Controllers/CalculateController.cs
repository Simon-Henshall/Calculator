using Calculator.API.Models;
using Serilog;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Calculator.API.Controllers
{
    public class CalculateController : ApiController
    {
        private double result;

        [SwaggerResponse(HttpStatusCode.OK, Description = "A response model", Type = typeof(ResponseModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid request model", Type = typeof(RequestCalculationModel))]
        [Route("api/Calculate")]
        [HttpPost]
        public IHttpActionResult Calculate(RequestCalculationModel currentOperation)
        {
            var badRequestResponse = new BadRequestResponseModel();

            if (!string.IsNullOrWhiteSpace(currentOperation.Symbol))
            {
                try
                {
                    switch (currentOperation.Symbol)
                    {
                        case Symbols.Plus:
                            result = currentOperation.Operand1 + currentOperation.Operand2;
                            break;
                        case Symbols.Minus:
                            result = currentOperation.Operand1 - currentOperation.Operand2;
                            break;
                        case Symbols.Times:
                            result = currentOperation.Operand1 * currentOperation.Operand2;
                            break;
                        case Symbols.Divide:
                            result = (double)currentOperation.Operand1 / currentOperation.Operand2;
                            break;
                    }
                    var successResponse = new SuccessResponseModel(result);
                    return Ok(successResponse);
                }
                catch (Exception ex)
                {
                    Log.Error("Exception was thrown", ex);
                    return Content(HttpStatusCode.BadRequest, ex);
                }
            }
            Log.Error("A bad request was provided", badRequestResponse);
            return Content(HttpStatusCode.BadRequest, badRequestResponse);
        }

        public IHttpActionResult ParseInput(InputModel input)
        {
            Log.Information("Input was", input);

            string[] components = Regex.Split(input.Input, @"([0-9]+)([\+\-X\/]+)([0-9]+)");
            // ToDo: Improve this to handle multiple inputs
            var parsedInput = new RequestCalculationModel
            {
                Operand1 = components.ElementAtOrDefault(1) != null ? int.Parse(components[1]) : 0,
                Symbol = components.ElementAtOrDefault(2) != null ? components[2] : "",
                Operand2 = components.ElementAtOrDefault(3) != null ? int.Parse(components[3]) : 0,
            };

            Log.Information("Parsed input was", parsedInput);

            return Calculate(parsedInput);
        }
    }
}
