using System.ComponentModel;

namespace SensorEvaluator.SensorEvaluators
{
    public enum SensorEvaluation
    {
        [Description("keep")]
        Keep,
        [Description("discard")]
        Discard,
        [Description("precise")]
        Precise,
        [Description("very precise")]
        VeryPrecise,
        [Description("ultra precise")]
        UltraPrecise,
    }
}
