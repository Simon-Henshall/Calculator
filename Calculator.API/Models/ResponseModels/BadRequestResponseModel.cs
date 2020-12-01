using System.Net;

namespace Calculator.API.Models
{
    public class BadRequestResponseModel : ResponseModel
    {
        public string CustomMessage { get; set; }

        public BadRequestResponseModel()
        {
            StatusCode = HttpStatusCode.BadRequest;
            StatusMessage = StatusCode.ToString();
            CustomMessage = "";
        }
    }
}