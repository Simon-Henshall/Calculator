using Calculator.API.Controllers;
using Calculator.API.Logic;
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

        public string ParseInput(string input)
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

            var request = calculator.ParseInput(testInput) as NegotiatedContentResult<BadRequestResponseModel>;
            var result = request.Content.CustomMessage;

            return result;
        }
        
        public double Calculate(int operand1, string symbol, int operand2)
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
        [TestCase(1, "+", 1, 2)]
        [TestCase(1, "+", 11, 12)]
        [TestCase(1, "+", -1, 0)]
        [TestCase(11, "+", 1, 12)]
        [TestCase(11, "+", 11, 22)]
        [TestCase(-1, "+", 1, 0)]
        [TestCase(-1, "+", -1, -2)]
        [TestCase(-1, "+", -11, -12)]
        [TestCase(-11, "+", -1, -12)]
        [TestCase(-11, "+", -11, -22)]
        [TestCase(0, "+", 1, 1)]
        [TestCase(1, "+", 0, 1)]
        public void TestAddition(int operand1, string symbol, int operand2, double expectedResult)
        {
            var result = API.Logic.CalculatorLogic.Calculate(operand1, symbol, operand2);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(1, "-", 1, 0)]
        [TestCase(1, "-", 11, -10)]
        [TestCase(1, "-", -1, 2)]
        [TestCase(11, "-", 1, 10)]
        [TestCase(11, "-", 11, 0)]
        [TestCase(-1, "-", 1, -2)]
        [TestCase(-1, "-", -1, 0)]
        [TestCase(-1, "-", -11, 10)]
        [TestCase(-11, "-", -1, -10)]
        [TestCase(-11, "-", -11, 0)]
        [TestCase(0, "-", 1, -1)]
        [TestCase(1, "-", 0, 1)]
        public void TestSubtraction(int operand1, string symbol, int operand2, double expectedResult)
        {
            var result = API.Logic.CalculatorLogic.Calculate(operand1, symbol, operand2);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(1, "X", 1, 1)]
        [TestCase(1, "X", 11, 11)]
        [TestCase(1, "X", -1, -1)]
        [TestCase(11, "X", 1, 11)]
        [TestCase(11, "X", 11, 121)]
        [TestCase(-1, "X", 1, -1)]
        [TestCase(-1, "X", -1, 1)]
        [TestCase(-1, "X", -11, 11)]
        [TestCase(-11, "X", -1, 11)]
        [TestCase(-11, "X", -11, 121)]
        [TestCase(0, "X", 1, 0)]
        [TestCase(1, "X", 0, 0)]
        public void TestMultiplication(int operand1, string symbol, int operand2, double expectedResult)
        {
            var result = API.Logic.CalculatorLogic.Calculate(operand1, symbol, operand2);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(1, "/", 1, 1)]
        [TestCase(1, "/", 11, 0)]
        [TestCase(1, "/", -1, -1)]
        [TestCase(11, "/", 1, 11)]
        [TestCase(11, "/", 11, 1)]
        [TestCase(-1, "/", 1, -1)]
        [TestCase(-1, "/", -1, 1)]
        [TestCase(-1, "/", -11, 0)]
        [TestCase(-11, "/", -1, 11)]
        [TestCase(-11, "/", -11, 1)]
        [TestCase(0, "/", 1, 0)]
        public void TestDivision(int operand1, string symbol, int operand2, double expectedResult)
        {
            var result = API.Logic.CalculatorLogic.Calculate(operand1, symbol, operand2);
            Assert.AreEqual(expectedResult, result);
        }
    }
    [TestFixture]
    public class FailureTests : BaseTest
    {
        [Test]
        [Ignore("These tests break due to log4net checking configuration")]
        [TestCase("X", "The number of symbols don't match the expected input")]
        [TestCase("22", "The number of symbols don't match the expected input")]
        public void InvalidInputs(string input, string msg)
        {
            var ex = Assert.Throws<InvalidNumberOfSymbolsException>(() => ParseInput(input));
            Assert.That(ex.Message, Is.EqualTo(msg));
        }

        [Test] // Only integers produce a DivideByZeroException - https://stackoverflow.com/a/44258269/2341603
        [TestCase(1, "/", 0, "Attempted to divide by zero.")]
        [TestCase(-1, "/", 0, "Attempted to divide by zero.")]
        public void DivisionByZero(int operand1, string symbol, int operand2, string expectedResult)
        {
            var ex = Assert.Throws<DivideByZeroException>(() => API.Logic.CalculatorLogic.Calculate(operand1, symbol, operand2));
            Assert.That(ex.Message, Is.EqualTo(expectedResult));
        }
    }
}