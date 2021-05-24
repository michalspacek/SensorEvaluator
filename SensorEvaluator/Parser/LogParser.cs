using SensorEvaluator.Exceptions;
using SensorEvaluator.SensorEvaluators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SensorEvaluator
{
    public class LogParser : ILogParser
    {
        private readonly ISensorEvaluatorFactory _sensorEvaluatorFactory;

        public LogParser(ISensorEvaluatorFactory sensorEvaluatorFactory)
        {
            _sensorEvaluatorFactory = sensorEvaluatorFactory;
        }

        public SensorReferenceValues GetReferenceValues(StreamReader streamReader)
        {
            var lineStr = streamReader.ReadLine();

            if (string.IsNullOrEmpty(lineStr))
            {
                throw new LogParserException("reference values not provided");
            }

            var items = lineStr.Split(' ');

            if (!items[0].Equals("reference"))
            {
                throw new LogParserException("reference values expected");
            }

            if (items.Count() != 4)
            {
                throw new LogParserException("first line does not contain 3 reference values");
            }

            return new SensorReferenceValues(double.Parse(items[1], CultureInfo.InvariantCulture), decimal.Parse(items[2], CultureInfo.InvariantCulture), int.Parse(items[3], CultureInfo.InvariantCulture));
        }

        public IEnumerable<ISensorEvaluator> GetSensorEvaluators(StreamReader streamReader)
        {
            ISensorEvaluator currentSensor = null;

            while (!streamReader.EndOfStream)
            {
                var lineStr = streamReader.ReadLine();
                var parsedLine = ParseLine(lineStr);

                DateTime logDateTime;
                if (DateTime.TryParse(parsedLine[0], out logDateTime))
                {
                    if (currentSensor == null)
                    {
                        throw new LogParserException("unexpected log entry");
                    }

                    currentSensor.AddDataLine(logDateTime, parsedLine[1]);

                    if (streamReader.EndOfStream)
                    {
                        yield return currentSensor;
                    }
                }
                else
                {
                    if (currentSensor != null)
                    {
                        yield return currentSensor;
                    }

                    currentSensor = _sensorEvaluatorFactory.GetSensorEvaluator(parsedLine[0], parsedLine[1]);
                }
            }
        }

        private string[] ParseLine(string lineStr)
        {
            var items = lineStr.Split(' ');
            if (items.Count() != 2)
            {
                throw new LogParserException("log parsing error");
            }

            return items;
        }
    }
}
