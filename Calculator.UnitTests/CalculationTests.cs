using Calculator.Models;
using Calculator.Controllers;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Newtonsoft.Json;

namespace Calculator.UnitTests
{
    public class Tests
    {
        // ToDo: Implement logging
        private ILogger _logger;
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CalculationTests()
        {
            // ToDo: Mock test data
            var testData = new RequestCalculationModel
            {
                Operand1 = 1,
                Symbol = "+",
                Operand2 = 1
            };

            var svc = new CalculatorController();
            var request = svc.Calculate(testData);
            // ToDo: There must be a better way to convert this
            var stringResult = JsonConvert.SerializeObject(request);
            var response = JsonConvert.DeserializeObject<ResponseModel>(stringResult);
            var calculationResponse = response.Value.Result;

            // Check the results
            Assert.AreEqual(2, calculationResponse);
        }
    }
}