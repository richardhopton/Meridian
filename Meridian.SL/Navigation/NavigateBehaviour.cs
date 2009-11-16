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
using System.Windows.Interactivity;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Meridian.SL.Navigation
{
    public class NavigateBehaviour : Behavior<FrameworkElement>
    {
        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public static readonly DependencyProperty UrlProperty = 
            DependencyProperty.Register("Url", typeof(string), typeof(NavigateBehaviour), new PropertyMetadata(string.Empty));

        public string Verb
        {
            get { return (string)GetValue(VerbProperty); }
            set { SetValue(VerbProperty, value); }
        }

        public static readonly DependencyProperty VerbProperty = 
            DependencyProperty.Register("Verb", typeof(string), typeof(NavigateBehaviour), new PropertyMetadata(RequestVerbs.Retrieve));

        public string Target
        {
            get { return (string)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(string), typeof(NavigateBehaviour), new PropertyMetadata(string.Empty));

        public ParameterCollection Parameters
        {
            get { return (ParameterCollection)GetValue(ParametersProperty); }
            set { SetValue(ParametersProperty, value); }
        }
        
        public static readonly DependencyProperty ParametersProperty =
            DependencyProperty.Register("Parameters", typeof(ParameterCollection), typeof(NavigateBehaviour), new PropertyMetadata(null));

        public NavigateBehaviour()
        {
            SetValue(ParametersProperty, new ParameterCollection());
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            if (DesignerProperties.GetIsInDesignMode(AssociatedObject)) 
                return;

            var element = AssociatedObject as FrameworkElement;

            //EvaluateBindings();

            var button = AssociatedObject as Button;
            if (button != null)
            {
                button.Click -= AssociatedObjectClick;
                button.Click += AssociatedObjectClick;
            }
            else
            {
                element.MouseLeftButtonUp -= AssociatedObjectMouseUp;
                element.MouseLeftButtonUp += AssociatedObjectMouseUp;
            }
        }

        //private void EvaluateBindings()
        //{
        //    var element = ((FrameworkElement)AssociatedObject);

        //    if (_internalParameters == null)
        //    {
        //        _internalParameters = new InternalParameterCollection();                
        //        foreach (var parameter in Parameters)
        //        {
        //            var internalElement = new Parameter();
        //            internalElement.ParameterName = parameter.ParameterName;
        //            var valueBinding = parameter.GetBindingExpression(Parameter.ValueProperty);
        //            //valueBinding.ParentBinding.BindsDirectlyToSource = true;
        //            //valueBinding.ParentBinding.Mode = BindingMode.TwoWay;
        //            //var valueBinding = BindingOperations.GetBinding(parameter, Parameter.ValueProperty);
        //            if (valueBinding == null)
        //            {
        //                internalElement.Value = parameter.Value;
        //            }
        //            else
        //            {
        //                internalElement.SetBinding(Parameter.ValueProperty, valueBinding.ParentBinding);
        //                //BindingOperations.SetBinding(internalElement, InternalParameter.ValueProperty, valueBinding);
        //            }
        //            _internalParameters.Add(internalElement);
        //        }
        //        NavigateBehaviour.SetParameters(AssociatedObject, _internalParameters);
        //        Parameters.Clear();
        //    }

            //return _internalParameters.Cast<InternalParameter>().ToDictionary(x => x.ParameterName, x => x.Value);
        //}

        private void AssociatedObjectClick(object sender, RoutedEventArgs e)
        {
            Navigate();
        }

        private void AssociatedObjectMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Navigate();
        }

        private void Navigate()
        {
            Frame frame = UIHelper.FindViewFrame(Application.Current.RootVisual, Target) as Frame;
            if (frame != null)
            {
                NavigationService.For(frame).Navigate(Url, GetParameters(this), Verb);
            }            
        }

        private RequestParameters GetParameters(NavigateBehaviour behaviour)
        {
            Requires.NotNull(behaviour, "behaviour");

            ParameterCollection parameterColl = ParameterCollection.GetParameters(AssociatedObject);

            RequestParameters parameters = new RequestParameters();
            foreach (var parameter in Parameters)
            {
                parameters.Add(parameter.ParameterName, parameter.Value);
            }
            return parameters;
        }
    }
}
