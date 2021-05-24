using System;

namespace SensorEvaluator.SensorEvaluators
{
    public interface ISensorEvaluator
    {
        public string Name { get; set; }
        
        public void AddDataLine(DateTime logDateTime, string value);

        public SensorEvaluation GetEvaluation(SensorReferenceValues sensorReferenceValues);
    }
}
