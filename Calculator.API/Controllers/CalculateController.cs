using Calculator.API.Models;
using CalcLibrary;
using log4net;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Results;

namespace Calculator.API.Controllers
{
    public class CalculateController : ApiController
    {
        private readonly ILog _log;
        
        public CalculateController()
        {
            _log = LogManager.GetLogger("calculator");
        }

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
                    var result = CalculatorLogic.Calculate(currentOperation.Operand1, currentOperation.Symbol, currentOperation.Operand2);
                    var successResponse = new SuccessResponseModel(result);
                    _log.Info($"Calculation successful");
                    return Ok(successResponse);
                }
                catch (Exception ex)
                {
                    _log.Error("Calculation failed", ex);
                    return Content(HttpStatusCode.BadRequest, ex);
                }
            }
            return Content(HttpStatusCode.BadRequest, badRequestResponse);
        }

        [SwaggerResponse(HttpStatusCode.OK, Description = "A response model", Type = typeof(ResponseModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid request model", Type = typeof(InputModel))]
        [Route("api/ParseInput")]
        [HttpPost]
        public IHttpActionResult ParseInput(InputModel input)
        {
            _log.Info($"Base input received was: {input.Input}");

            // Parse the input into an array of string componetns
            string[] components = Regex.Split(input.Input, @"([0-9]+)([\+\-X\/]+)([0-9]+)");

            // Remove the first and last (blank) elements from the array
            components = components.Skip(1).ToArray();
            components = components.Take(components.Count() - 1).ToArray();
            _log.Info("Components were interpreted as: " + string.Join(", ", components));

            // Assuming that we follow a "[operand]([operator][operand] * N)" pattern then it is (2N + 1)
            // Assuming that negative numbers are not allowed
            if (components.Length % 2 == 0)
            {
                // There can never be an even number of components
                _log.Error("The number of symbols don't match the expected input");
                throw new InvalidNumberOfSymbolsException("The number of symbols don't match the expected input");
            }

            // Process the input
            int i = 0;
            double finalResult = 0;
            while (i < components.Length - 2) // We process three at once, so subtract two from the loop
            {
                var parsedInput = new RequestCalculationModel
                {
                    Operand1 = components.ElementAtOrDefault(i) != null ? int.Parse(components[i]) : 0,
                    Symbol = components.ElementAtOrDefault(i + 1) != null ? components[i + 1] : "",
                    Operand2 = components.ElementAtOrDefault(i + 2) != null ? int.Parse(components[i + 2]) : 0,
                };

                _log.Debug($"Operator 1 was: {parsedInput.Operand1}");
                _log.Debug($"Symbol was: {parsedInput.Symbol}");
                _log.Debug($"Operator 2 was: {parsedInput.Operand2}");

                // Validate the input
                ValidationContext vc = new ValidationContext(parsedInput);
                ICollection<ValidationResult> results = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(parsedInput, vc, results, true);

                // Valid input
                if (isValid && !string.IsNullOrEmpty(parsedInput.Symbol))
                {
                    _log.Debug($"Input was valid");
                    var immediateResult = Calculate(parsedInput) as OkNegotiatedContentResult<SuccessResponseModel>;
                    if (immediateResult != null)
                    {
                        _log.Info($"The immediate result was: {immediateResult.Content.Result}");
                        finalResult += immediateResult.Content.Result;
                    }
                }
                // Invalid input
                else
                {
                    _log.Error($"Input was invalid");
                    var badRequestResponse = new BadRequestResponseModel();
                    badRequestResponse.CustomMessage = "Unable to validate request model";
                    return Content(HttpStatusCode.BadRequest, badRequestResponse);
                }

                // Incremeber the count in sets of 4
                i += 4;
            }

            // Return the final calculation
            _log.Error($"Final result was: {finalResult}");
            return Ok(finalResult);
        }
    }
}
