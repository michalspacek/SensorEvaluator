using SensorEvaluator.Exceptions;
using System;
using System.Globalization;
using System.Linq;

namespace SensorEvaluator.SensorEvaluators
{
    public class HumiditySensorEvaluator : SensorEvaluatorBase<decimal>
    {                
        private const decimal HumidityTolerance = 1;        

        public override void AddDataLine(DateTime logDateTime, string value)
        {
            decimal sensorValue;

            if (!decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out sensorValue))
            {
                throw new LogParserException("invalid value");
            }

            SensorValues.Add(sensorValue);
        }

        public override SensorEvaluation GetEvaluation(SensorReferenceValues sensorReferenceValues)
        {
            if (!SensorValues.Any())
            {
                throw new LogParserException("values not provided");
            }

            if (SensorValues.All(x => x <= sensorReferenceValues.Humidity + HumidityTolerance && x >= sensorReferenceValues.Humidity - HumidityTolerance)) {
                return SensorEvaluation.Keep;
            }
            return SensorEvaluation.Discard;
        }
    }
}
