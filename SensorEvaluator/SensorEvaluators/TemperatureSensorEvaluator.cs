using SensorEvaluator.Exceptions;
using SensorEvaluator.Utils;
using System;
using System.Globalization;
using System.Linq;

namespace SensorEvaluator.SensorEvaluators
{
    public class TemperatureSensorEvaluator : SensorEvaluatorBase<double>
    {               
        private const double TempTolerance = 0.5;
        private const double UltraPreciseDeviation = 3;
        private const double VeryPreciseDeviation = 5;

        public override void AddDataLine(DateTime logDateTime, string value)
        {
            double sensorValue;

            if (!double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out sensorValue))
            {
                throw new LogParserException("invalid value");
            }

            SensorValues.Add(sensorValue);
        }

        public override SensorEvaluation GetEvaluation(SensorReferenceValues sensorReferenceValues)
        {
            if (!SensorValues.Any())
            {
                throw new LogParserException("Values not provided");
            }

            var avgReading = SensorValues.Average();
            if (avgReading <= sensorReferenceValues.Temperature + TempTolerance && avgReading >= sensorReferenceValues.Temperature - TempTolerance)
            {
                var stdDevination = SensorValues.StandardDeviation();

                if (stdDevination < UltraPreciseDeviation)
                {
                    return SensorEvaluation.UltraPrecise;
                }
                else if (stdDevination < VeryPreciseDeviation)
                {
                    return SensorEvaluation.VeryPrecise;
                }
            }

            return SensorEvaluation.Precise;
        }
    }
}
