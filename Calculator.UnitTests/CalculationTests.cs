using Calculator.API.Controllers;
using Calculator.API.Models;
using FizzWare.NBuilder;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
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
        public List<T> CreateOperands<T>(params T[] testOperands)
        {
            Assert.That(testOperands, Is.TypeOf<int>());
            return new List<T>(testOperands);
        }

        //public Symbols[] CreateOperators(params string[] testOperators)
        //{
        //    var validSymbols = new Symbols[0];
        //    validSymbols.Append("+");
        //    foreach (string testOperator in testOperators)
        //    {
        //        Assert.That(validSymbols, Contains.Item(testOperator));
        //    }
            
        //    return validSymbols;
        //}

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
}