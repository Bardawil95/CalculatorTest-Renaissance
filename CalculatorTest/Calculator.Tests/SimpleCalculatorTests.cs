using Calculator.Core;
using NUnit.Framework;
using System;

namespace Calculator.Tests
{
    [TestFixture]
    public class SimpleCalculatorTests
    {
        private ISimpleCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new SimpleCalculator();
        }

        // --- SUCCESS PATHS ---

        [TestCase(10, 5, 15)]       // Positve numbers addition
        [TestCase(-10, -5, -15)]    // Negative numbers addition
        [TestCase(-5, 5, 0)]        // zero addition result
        public void Add_ValidInputs_ReturnsCorrectSum(int start, int amount, int expected)
        {
            var result = _calculator.Add(start, amount);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(10, 5, 5)]        // Positive numbers subtraction
        [TestCase(5, 10, -5)]       // Negative numbers subtraction
        [TestCase(100, 50, 50)]     // zero addition result
        public void Subtract_ValidInputs_ReturnsCorrectDifference(int start, int amount, int expected)
        {
            var result = _calculator.Subtract(start, amount);
            Assert.That(result, Is.EqualTo(expected));
        }

        // --- NEGATIVE PATHS (Checking that wrong answers are NOT returned) ---

        [TestCase(10, 5, 51)]
        [TestCase(-5, 5, 1)]
        public void Add_IncorrectInputs_DoesNotMatch(int start, int amount, int incorrectExpected)
        {
            var result = _calculator.Add(start, amount);
            // We use Is.Not.EqualTo because we WANT this to pass when the math is different
            Assert.That(result, Is.Not.EqualTo(incorrectExpected));
        }

        [TestCase(10, 5, 99)]
        public void Subtract_IncorrectInputs_DoesNotMatch(int start, int amount, int incorrectExpected)
        {
            var result = _calculator.Subtract(start, amount);
            Assert.That(result, Is.Not.EqualTo(incorrectExpected));
        }

        // --- EXCEPTION PATHS (The "Incorrect" Data Limits) ---

        [Test]
        public void Add_Overflow_ThrowsOverflowException()
        {
            // Verifies the 'checked' keyword logic in your service
            Assert.Throws<OverflowException>(() => _calculator.Add(int.MaxValue, 1));
        }

        [Test]
        public void Subtract_Underflow_ThrowsOverflowException()
        {
            // Verifies the 'checked' keyword logic for minimum integer limits
            Assert.Throws<OverflowException>(() => _calculator.Subtract(int.MinValue, 1));
        }
    }
}