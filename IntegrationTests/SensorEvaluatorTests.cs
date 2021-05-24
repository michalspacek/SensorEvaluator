using SensorEvaluator;
using SensorEvaluator.Exceptions;
using System.IO;
using Xunit;

namespace IntegrationTests
{
    public class SensorEvaluatorTests
    {
        [Fact]
        public void SampleLogShouldProduceCorrectOutput()
        {
            var logContentStr = File.ReadAllText("SampleLog.txt");
            var sampleOutput = File.ReadAllText("SampleOutput.txt");
            var result = SensorEvaluator.SensorEvaluator.EvaluateLogFile(logContentStr);

            Assert.Equal(result, sampleOutput);
        }

        [Fact]
        public void EmptyLogShouldThrowException()
        {
            var logContentStr = string.Empty;

            Assert.Throws<LogParserException>(() => SensorEvaluator.SensorEvaluator.EvaluateLogFile(logContentStr));
        }
    }
}
