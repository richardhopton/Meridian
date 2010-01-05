using System;
using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Meridian.SL
{
    public class ParameterCollection : ObservableCollection<Parameter>
    {
        public RequestParameters ToRequestParameters(FrameworkElement parent)
        {
            Requires.NotNull(parent, "parent");

            var requestParameters = new RequestParameters();
            foreach (var parameter in this)
            {
                if (parameter.Value != null)
                {
                    requestParameters.Add(parameter.ParameterName, parameter.Value);
                }
                else if (!string.IsNullOrEmpty(parameter.Path))
                {
                    var element = string.IsNullOrEmpty(parameter.ElementName) ? parent : parent.FindName(parameter.ElementName);
                    object value = null;
                    if (element != null)
                    {
                        string[] propertyPath = parameter.Path.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
                        value = GetPropertyValue(element, propertyPath);                        
                    }
                    requestParameters.Add(parameter.ParameterName, value);
                }                
            }
            return requestParameters;
        }
        
        private object GetPropertyValue(object element, IEnumerable<string> propertyPath)
        {
            Requires.NotNull(element, "element");
            Requires.NotNull(propertyPath, "propertyPath");

            object propertyValue = element;
            foreach (var s in propertyPath)
            {
                propertyValue = GetPropertyValue(propertyValue, s);
            }
            return propertyValue;
        }

        private object GetPropertyValue(object element, string property)
        {
            Requires.NotNull(element, "element");
            Requires.NotNullOrEmpty(property, "property");

            var propertyInfo = element.GetType().GetProperty(property);
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(element, null);
            }
            return null;
        }
    }
}
