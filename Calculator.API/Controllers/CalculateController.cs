using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Web.Http;
using Calculator.Models;
using System.Net.Http;

namespace Calculator.API.Controllers
{
    public class CalculateController : ApiController
    {
        private int result;

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
                            result = request.Operand1 / request.Operand2;
                            break;
                    }
                    var successResponse = new SuccessResponseModel<ResponseCalculationModel>(result);
                    return ResponseMessage(Request.CreateResponse(successResponse.StatusCode, successResponse));
                }
                catch (Exception)
                {
                    return ResponseMessage(Request.CreateResponse(badRequestResponse.StatusCode, badRequestResponse));
                }
            }
            return ResponseMessage(Request.CreateResponse(badRequestResponse.StatusCode, badRequestResponse));
        }
    }
}
