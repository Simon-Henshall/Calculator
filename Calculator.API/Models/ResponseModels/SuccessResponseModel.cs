using System.Net;

namespace Calculator.API.Models
{
    public class SuccessResponseModel: ResponseModel
    {
        public SuccessResponseModel(double result)
        {
            Result = result;
            StatusCode = HttpStatusCode.OK;
            StatusMessage = StatusCode.ToString();
        }
    }
}
