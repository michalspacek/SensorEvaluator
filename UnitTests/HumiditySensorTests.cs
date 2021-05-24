using SensorEvaluator.Exceptions;
using SensorEvaluator.SensorEvaluators;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class HumiditySensorTests
    {
        [Theory]
        [InlineData(new string[] { "45.2", "45.3", "45.1" }, 45.0, SensorEvaluation.Keep)]
        [InlineData(new string[] { "44.4", "43.9", "44.9", "43.8", "42.1" }, 45.0, SensorEvaluation.Discard)]      
        public void HumiditySensorShouldReturnCorrectEvaluation(IEnumerable<string> inputValues, decimal reference, SensorEvaluation result)
        {
            var sut = new HumiditySensorEvaluator();

            foreach (var inputValue in inputValues)
            {
                sut.AddDataLine(new DateTime(), inputValue);
            }

            Assert.Equal(result, sut.GetEvaluation(new SensorReferenceValues(0, reference, 0)));
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
