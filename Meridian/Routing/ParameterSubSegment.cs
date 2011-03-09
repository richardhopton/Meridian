using System;

namespace Meridian.Routing
{
    internal class ParameterSubSegment : PathSubSegment
    {
        public String ParameterName { get; set; }

        public ParameterSubSegment(String parameterName)
        {
            ParameterName = parameterName;
        }
    }
}