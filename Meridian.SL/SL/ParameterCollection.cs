using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Meridian.SL
{
    public class ParameterCollection : ObservableCollection<Parameter>
    {
        public RequestParameters ToRequestParameters()
        {
            return ToRequestParameters(null);    
        }

        public RequestParameters ToRequestParameters(FrameworkElement parent)
        {
            var requestParameters = new RequestParameters();
            foreach (var parameter in this)
            {
                if (parameter.Value != null)
                {
                    requestParameters.Add(parameter.ParameterName, parameter.Value);
                }
                else if (!String.IsNullOrEmpty(parameter.Path) && (parent != null))
                {
                    var element = String.IsNullOrEmpty(parameter.ElementName) ? parent : parent.FindName(parameter.ElementName);
                    Object value = null;
                    if (element != null)
                    {
                        String[] propertyPath = parameter.Path.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
                        value = GetPropertyValue(element, propertyPath);                        
                    }
                    requestParameters.Add(parameter.ParameterName, value);
                }                
            }
            return requestParameters;
        }
        
        private Object GetPropertyValue(Object element, IEnumerable<String> propertyPath)
        {
            Requires.NotNull(element, "element");
            Requires.NotNull(propertyPath, "propertyPath");

            Object propertyValue = element;
            foreach (var s in propertyPath)
            {
                propertyValue = GetPropertyValue(propertyValue, s);
            }
            return propertyValue;
        }

        private Object GetPropertyValue(Object element, String property)
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

        public void Add(String parameterName, Object value)
        {
            Requires.NotNullOrEmpty(parameterName, "parameterName");
            
            Add(new Parameter(parameterName, value));
        }
    }
}
