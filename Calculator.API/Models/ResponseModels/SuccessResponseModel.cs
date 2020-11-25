using System.Net;

namespace Calculator.API.Models
{
    public class SuccessResponseModel<T> : ResponseModel
    {
        public object Result { get; internal set; }

        public SuccessResponseModel(T result)
        {
            Result = result;
            Messages.Add("Resource found");
            StatusCode = HttpStatusCode.OK;
            StatusMessage = StatusCode.ToString();
        }

        public SuccessResponseModel(int result)
        {
            Result = result;
        }
    }
}
