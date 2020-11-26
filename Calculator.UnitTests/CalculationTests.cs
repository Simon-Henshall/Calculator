using Calculator.API.Controllers;
using Calculator.API.Models;
using FizzWare.NBuilder;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Web.Http;
using System.Web.Http.Results;

namespace Calculator.UnitTests
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2187:TestCases should contain tests", Justification = "<Pending>")]
    public class BaseTest
    {
        // ToDo: Implement logging
        private ILogger _logger;

        public dynamic ParseInput(string input)
        {
            var calculator = new CalculateController()
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var testInput = new InputModel
            {
                Input = input
            };

            var request = calculator.ParseInput(testInput) as OkNegotiatedContentResult<SuccessResponseModel>;
            var result = request.Content.Result;

            return result;
        }
        
        public dynamic Calculate(int operand1, string symbol, int operand2)
        {
            var testData = Builder<RequestCalculationModel>
                .CreateNew()
                .With(r => r.Operand1 = operand1)
                .And(r => r.Symbol = symbol)
                .And(r => r.Operand2 = operand2)
                .Build();

            var calculator = new CalculateController()
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var request = calculator.Calculate(testData) as OkNegotiatedContentResult<SuccessResponseModel>;
            var result = request.Content.Result;

            return result;
        }
    }

    [TestFixture]
    public class SuccessTests : BaseTest
    {
        [Test]
        public void TestAddition()
        {
            var random = new Random();
            var testedSymbol = "+";

            var result = Calculate(random.Next(), testedSymbol, random.Next());
            Assert.That(result, Is.TypeOf<double>());
            Assert.That(result % 1 == 0);
        }

        [Test]
        public void TestSubtraction()
        {
            var random = new Random();
            var testedSymbol = "-";

            var result = Calculate(random.Next(), testedSymbol, random.Next());
            Assert.That(result, Is.TypeOf<double>());
            Assert.That(result % 1 == 0);
        }

        [Test]
        public void TestMultiplication()
        {
            var random = new Random();
            var testedSymbol = "X";

            var result = Calculate(random.Next(), testedSymbol, random.Next());
            Assert.That(result, Is.TypeOf<double>());
            Assert.That(result % 1 == 0);
        }

        [Test]
        public void TestDivision()
        {
            var random = new Random();
            var testedSymbol = "/";

            var result = Calculate(random.Next(), testedSymbol, random.Next());
            Assert.That(result, Is.TypeOf<double>());
            Assert.That(result % 1 != 0);
            
        }
    }
    [TestFixture]
    public class FailureTests : BaseTest
    {
        [Test]
        public void NoOperand()
        {
            var ex = Assert.Throws<NullReferenceException>(() => ParseInput("X"));
            Assert.That(ex.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }

        [Test]
        public void NoOperator()
        {
            var ex = Assert.Throws<NullReferenceException>(() => ParseInput("22"));
            Assert.That(ex.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }
    }
}