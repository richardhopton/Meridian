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
using Meridian.SL.Navigation;

namespace Meridian.SL
{
    public class Request : DependencyObject
    {
        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register("Url", typeof(string), typeof(Request), new PropertyMetadata(string.Empty));

        public string Verb
        {
            get { return (string)GetValue(VerbProperty); }
            set { SetValue(VerbProperty, value); }
        }

        public static readonly DependencyProperty VerbProperty =
            DependencyProperty.Register("Verb", typeof(string), typeof(Request), new PropertyMetadata(string.Empty));

        public string Target
        {
            get { return (string)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }
        
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(string), typeof(Request), new PropertyMetadata(string.Empty));

        public ParameterCollection Parameters
        {
            get { return (ParameterCollection)GetValue(ParametersProperty); }
            set { SetValue(ParametersProperty, value); }
        }
        
        public static readonly DependencyProperty ParametersProperty =
            DependencyProperty.Register("Parameters", typeof(ParameterCollection), typeof(Request), new PropertyMetadata(null));

        public Request()
        {
            SetValue(ParametersProperty, new ParameterCollection());
        }
    }
}
