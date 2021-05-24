using SensorEvaluator.Exceptions;

namespace SensorEvaluator.SensorEvaluators
{
    class SensorEvaluatorFactory : ISensorEvaluatorFactory
    {
        public ISensorEvaluator GetSensorEvaluator(string sensorType, string sensorName)
        {
            switch (sensorType) {  
                case "thermometer":
                    return new TemperatureSensorEvaluator { Name = sensorName };
                case "humidity":
                    return new HumiditySensorEvaluator { Name = sensorName };
                case "monoxide":
                    return new CarbonMonoxideSensorEvaluator { Name = sensorName };
                default:
                    throw new LogParserException("unsupported sensor type");
            }
        }
    }
}
