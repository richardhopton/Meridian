using System;
using System.Windows;

namespace Meridian.SL
{
    public class Parameter : FrameworkElement
    {       
        public String ParameterName { get; set; }

        public String ElementName { get; set; }

        public String Path { get; set; }

        public Object Value { get; set; }

        public Parameter()
        {            
        }

        public Parameter(String parameterName, Object value)
        {
            Requires.NotNullOrEmpty(parameterName, "parameterName");

            ParameterName = parameterName;
            Value = value;
        }
    }
}
