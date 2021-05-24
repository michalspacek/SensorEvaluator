namespace SensorEvaluator.SensorEvaluators
{
    public class SensorReferenceValues
    {
        public SensorReferenceValues(double temperature, decimal humidity, int carbonMonoxideLevel)
        {
            Temperature = temperature;
            Humidity = humidity;
            CarbonMonoxideLevel = carbonMonoxideLevel;
        }

        public double Temperature { get; }

        public decimal Humidity { get; }

        public int CarbonMonoxideLevel { get;  }
    }
}
