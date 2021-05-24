using SensorEvaluator.Exceptions;
using SensorEvaluator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SensorEvaluator.SensorEvaluators
{
    public class CarbonMonoxideSensorEvaluator : SensorEvaluatorBase<int>
    {                
        private const int CarbonMonoxideLevelTolerance = 3;

        public override void AddDataLine(DateTime logDateTime, string value)
        {
            int sensorValue;

            if (!int.TryParse(value, out sensorValue))
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

            if (SensorValues.All(x => x <= sensorReferenceValues.CarbonMonoxideLevel + CarbonMonoxideLevelTolerance && x >= sensorReferenceValues.CarbonMonoxideLevel - CarbonMonoxideLevelTolerance))
            {
                return SensorEvaluation.Keep;
            }
            return SensorEvaluation.Discard;
        }
    }
}
