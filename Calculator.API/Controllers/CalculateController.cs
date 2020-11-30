using Calculator.API.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                    var result = Logic.CalculatorLogic.Calculate(currentOperation.Operand1, currentOperation.Symbol, currentOperation.Operand2);
                    var successResponse = new SuccessResponseModel(result);
                    return Ok(successResponse);
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.BadRequest, ex);
                }
            }
            return Content(HttpStatusCode.BadRequest, badRequestResponse);
        }

        public IHttpActionResult ParseInput(InputModel input)
        {

            string[] components = Regex.Split(input.Input, @"([0-9]+)([\+\-X\/]+)([0-9]+)");
            // ToDo: Improve this to handle multiple inputs
            var parsedInput = new RequestCalculationModel
            {
                Operand1 = components.ElementAtOrDefault(1) != null ? int.Parse(components[1]) : 0,
                Symbol = components.ElementAtOrDefault(2) != null ? components[2] : "",
                Operand2 = components.ElementAtOrDefault(3) != null ? int.Parse(components[3]) : 0,
            };

           return Calculate(parsedInput);
        }
    }
}
