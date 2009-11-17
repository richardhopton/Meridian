using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Meridian.SL
{
    public class Parameter : FrameworkElement
    {
        public string ParameterName { get; set; }

        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
      
        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register("Value", typeof(object), typeof(Parameter), new PropertyMetadata(null));
    }
}
