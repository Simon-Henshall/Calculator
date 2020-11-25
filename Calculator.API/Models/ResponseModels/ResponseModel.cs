using System.Collections.Generic;
using System.Net;

namespace Calculator.API.Models
{
    public class ResponseModel
    {
        public HttpStatusCode StatusCode { get; internal set; }
        public string StatusMessage { get; internal set; }
        public List<string> Messages { get; set; }

        public ResponseModel()
        {
            Messages = new List<string>();
        }
    }
}