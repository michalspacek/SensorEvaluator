using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SensorEvaluator.Utils
{
    static class Extensions
    {
        public static double StandardDeviation(this IEnumerable<double> values)
        {
            var standardDeviation = 0.0;
            var count = values.Count();

            if (count > 0)
            {
                var avg = values.Average();
                var sum = values.Sum(x => (x - avg) * (x - avg));
                standardDeviation = Math.Sqrt(sum / count);
            }
            return standardDeviation;
        }

        public static string ToDescription(this Enum en)
        {
            var type = en.GetType();

            var memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }
    }
}
