using SensorEvaluator.Exceptions;
using SensorEvaluator.SensorEvaluators;
using SensorEvaluator.Utils;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace SensorEvaluator
{
    public class SensorEvaluator
    {
        public static string EvaluateLogFile(string logContentsStr)
        {
            var sensorEvaluations = new Dictionary<string, string>();
            var sensorFactory = new SensorEvaluatorFactory();
            var logParser = new LogParser(sensorFactory);

            using (var streamReader = new StreamReader(GetStreamFromString(logContentsStr)))
            {
                var sensorReferenceValues = logParser.GetReferenceValues(streamReader);
                foreach (var sensor in logParser.GetSensorEvaluators(streamReader))
                {
                    AddSensorEvaluation(sensorEvaluations, sensor, sensorReferenceValues);
                }             
            }

            var options =  new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(sensorEvaluations, options);
        }

        private static void AddSensorEvaluation(Dictionary<string, string> sensorEvaluations, ISensorEvaluator currentSensor, SensorReferenceValues sensorReferenceValues)
        {
            if (sensorEvaluations.ContainsKey(currentSensor.Name))
            {
                throw new LogParserException("duplicate sensor name");
            }
            sensorEvaluations.Add(currentSensor.Name, currentSensor.GetEvaluation(sensorReferenceValues).ToDescription());
        }

        private static MemoryStream GetStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }
    }
}
