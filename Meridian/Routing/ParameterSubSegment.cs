namespace Meridian.Routing
{
    internal class ParameterSubSegment : PathSubSegment
    {
        public string ParameterName { get; set; }

        public ParameterSubSegment(string parameterName)
        {
            ParameterName = parameterName;
        }
    }
}