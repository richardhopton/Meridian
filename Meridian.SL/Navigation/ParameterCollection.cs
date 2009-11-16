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
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Meridian.SL.Navigation
{
    public class ParameterCollection : ObservableCollection<Parameter>
    {
        public static readonly DependencyProperty ParametersProperty =
            DependencyProperty.RegisterAttached("Parameters", typeof(ParameterCollection),
            typeof(NavigateBehaviour), new PropertyMetadata(null));
        
        public static ParameterCollection GetParameters(UIElement element)
        {
            return (ParameterCollection)element.GetValue(ParametersProperty);
        }

        public static void SetParameters(UIElement element, ParameterCollection value)
        {
            element.SetValue(ParametersProperty, value);
        }
    }
}
