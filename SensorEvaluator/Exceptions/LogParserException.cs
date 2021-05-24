using System;

namespace SensorEvaluator.Exceptions
{
    public class LogParserException : Exception
    {
        public LogParserException()
        {
        }

        public LogParserException(string message) : base(message)
        {
        }
    }
}
