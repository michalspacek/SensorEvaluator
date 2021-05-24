using System;
using System.Collections.Generic;

namespace SensorEvaluator.SensorEvaluators
{
    public abstract class SensorEvaluatorBase<T> : ISensorEvaluator
    {
        public string Name { get; set; }

        protected List<T> SensorValues = new List<T>();

        public abstract void AddDataLine(DateTime logDateTime, string value);

        public abstract SensorEvaluation GetEvaluation(SensorReferenceValues sensorReferenceValues);
    }
}
