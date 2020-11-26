using System.Collections.Generic;
using System.Net;

namespace Calculator.API.Models
{
    public class BadRequestResponseModel : ResponseModel
    {
        public List<KeyValuePair<string, string>> ModelErrors { get; set; }

        public BadRequestResponseModel()
        {
            StatusCode = HttpStatusCode.BadRequest;
            StatusMessage = StatusCode.ToString();
            ModelErrors = new List<KeyValuePair<string, string>>();
        }
    }
}