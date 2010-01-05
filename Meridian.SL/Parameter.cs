using System.Windows;

namespace Meridian.SL
{
    public class Parameter : FrameworkElement
    {       
        public string ParameterName { get; set; }

        public string ElementName { get; set; }

        public string Path { get; set; }

        public object Value { get; set; }
    }
}
