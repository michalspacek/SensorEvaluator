using SensorEvaluator.Exceptions;
using SensorEvaluator.SensorEvaluators;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class TemperatureSensorTests
    {
        [Theory]
        [InlineData(new string[] { "69.5", "70.1", "71.3" }, 70.0, SensorEvaluation.UltraPrecise)]
        [InlineData(new string[] { "72.5", "79.1", "65.2" }, 70.0, SensorEvaluation.Precise)]
        [InlineData(new string[] { "66.5", "71.2", "73.8" }, 70.0, SensorEvaluation.VeryPrecise)]
        public void TemperatureSensorShouldReturnCorrectEvaluation(IEnumerable<string> inputValues, double reference, SensorEvaluation result)
        {
            var sut = new TemperatureSensorEvaluator();

            foreach (var inputValue in inputValues)
            {
                sut.AddDataLine(new DateTime(), inputValue);
            }

            Assert.Equal(result, sut.GetEvaluation(new SensorReferenceValues (reference, 0, 0)));
        }

        [Fact]
        public void InvalidInputValuesShouldThrowException()
        {
            var logContentStr = string.Empty;

            var sut = new CarbonMonoxideSensorEvaluator();

            Assert.Throws<LogParserException>(() => sut.AddDataLine(new DateTime(), "x"));
        }
    }
}
