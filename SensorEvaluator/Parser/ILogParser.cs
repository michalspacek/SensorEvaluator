using System.Collections.Generic;
using System.IO;

namespace SensorEvaluator.SensorEvaluators
{
    public interface ILogParser
    {
        SensorReferenceValues GetReferenceValues(StreamReader streamReader);

        IEnumerable<ISensorEvaluator> GetSensorEvaluators(StreamReader streamReader);
    }
}
