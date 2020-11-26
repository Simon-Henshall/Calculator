using Calculator.API.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult Calculate(RequestCalculationModel request)
        {
            var badRequestResponse = new BadRequestResponseModel();

            if (!string.IsNullOrWhiteSpace(request.Symbol))
            {
                try
                {
                    switch (request.Symbol)
                    {
                        case Symbols.Plus:
                            result = request.Operand1 + request.Operand2;
                            break;
                        case Symbols.Minus:
                            result = request.Operand1 - request.Operand2;
                            break;
                        case Symbols.Times:
                            result = request.Operand1 * request.Operand2;
                            break;
                        case Symbols.Divide:
                            result = (double)request.Operand1 / request.Operand2;
                            break;
                    }
                    var successResponse = new SuccessResponseModel(result);
                    return Ok(successResponse);
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.BadRequest, ex);
                }
            }
            return ResponseMessage(Request.CreateResponse(badRequestResponse.StatusCode, badRequestResponse));
        }
    }
}
