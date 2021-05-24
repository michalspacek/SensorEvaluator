using SensorEvaluator.Exceptions;
using SensorEvaluator.SensorEvaluators;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class CarbonMonoxideSensorTests
    {
        [Theory]
        [InlineData(new string[] { "5", "7", "9" }, 6, SensorEvaluation.Keep)]
        [InlineData(new string[] { "2", "4", "10", "8", "6" }, 6, SensorEvaluation.Discard)]
        public void CarbonMonoxideSensorShouldReturnCorrectEvaluation(IEnumerable<string> inputValues, int reference, SensorEvaluation result)
        {
            var sut = new CarbonMonoxideSensorEvaluator();

            foreach (var inputValue in inputValues)
            {
                sut.AddDataLine(new DateTime(), inputValue);
            }

            Assert.Equal(result, sut.GetEvaluation(new SensorReferenceValues(0, 0, reference)));
        }

        [Fact]
        public void InvalidInputValuesShouldThrowException()
        {
            var logContentStr = string.Empty;

            var sut = new CarbonMonoxideSensorEvaluator();

            Assert.Throws<LogParserException>(() => sut.AddDataLine(new DateTime(), "11.1"));
        }
    }
}
