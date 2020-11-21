namespace Calculator.Models
{
    public class ResponseModel
    {
        public string ContentType { get; set; }

        public string SerializerSettings { get; set; }
        public string StatusCode { get; set; }
        public ResponseCalculationModel Value { get; set; }
    }
}
