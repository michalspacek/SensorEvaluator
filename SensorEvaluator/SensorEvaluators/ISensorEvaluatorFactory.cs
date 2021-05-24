namespace SensorEvaluator.SensorEvaluators
{
    public interface ISensorEvaluatorFactory
    {
        ISensorEvaluator GetSensorEvaluator(string sensorType, string sensorName);
    }
}
