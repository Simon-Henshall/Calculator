using Calculator.Controllers;
using Calculator.Models;
using Calculator.Interfaces;
using FizzWare.NBuilder;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

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
            var random = new Random();
            var validSymbols = new[] {"+", "-", "X", "/"};
            var testSymbols = new[] {"+", "-", "X", "/", "C", "=", "Not a value"};
            var testedSymbol = testSymbols[random.Next(testSymbols.Length)];

            var testData = Builder<RequestCalculationModel>
                .CreateNew()
                .With(r => r.Operand1 = random.Next())
                .And(r => r.Symbol = testedSymbol)
                .And(r => r.Operand2 = random.Next())
                .Build();

            var svc = new CalculatorController();


            // Chek the inputs
            var request = svc.Calculate(testData);
            if (request is BadRequestObjectResult)
            {
                var requestValue = (ObjectResult)request;
                Assert.That(requestValue.Value.ToString().Contains("Invalid symbol"));
                return;
            }

            Assert.That(testData.Operand1, Is.TypeOf<int>());
            Assert.That(testData.Symbol, Is.TypeOf<string>());
            Assert.That(testData.Operand2, Is.TypeOf<int>());

            // ToDo: There must be a better way to convert this
            var stringResult = JsonConvert.SerializeObject(request);

            // Check the output
            var response = JsonConvert.DeserializeObject<ResponseModel>(stringResult);
            if(Array.IndexOf(validSymbols, testedSymbol) == -1)
            {
                Assert.That(int.Parse(response.StatusCode) == (int)HttpStatusCode.BadRequest);
            }
            else
            {
                var calculationResponse = response.Value.Result;
                Assert.That(calculationResponse, Is.TypeOf<double>());
            }
        }
    }
}