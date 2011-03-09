using System;
using System.Windows;

namespace Meridian.SL
{
    public class Request : FrameworkElement
    {
        public String Url
        {
            get { return (String)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register("Url", typeof(String), typeof(Request), new PropertyMetadata(String.Empty));

        public String Verb
        {
            get { return (String)GetValue(VerbProperty); }
            set { SetValue(VerbProperty, value); }
        }

        public static readonly DependencyProperty VerbProperty =
            DependencyProperty.Register("Verb", typeof(String), typeof(Request), new PropertyMetadata(RequestVerbs.Retrieve));

        public String Target
        {
            get { return (String)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }
        
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(String), typeof(Request), new PropertyMetadata(String.Empty));

        public ParameterCollection Parameters
        {
            get { return (ParameterCollection)GetValue(ParametersProperty); }
            set { SetValue(ParametersProperty, value); }
        }
        
        public static readonly DependencyProperty ParametersProperty =
            DependencyProperty.Register("Parameters", typeof(ParameterCollection), typeof(Request), new PropertyMetadata(null));

        public Request()
        {
            SetValue(UrlProperty, Application.Current.Host.NavigationState);
            SetValue(ParametersProperty, new ParameterCollection());
        }
    }
}
