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
                if (!string.IsNullOrEmpty(parameter.Path))
                {
                    var element = string.IsNullOrEmpty(parameter.ElementName) ? parent : parent.FindName(parameter.ElementName);
                    object value = null;
                    if (element != null)
                    {
                        var propertyInfo = element.GetType().GetProperty(parameter.Path);
                        if (propertyInfo != null)
                        {
                            value = propertyInfo.GetValue(element, null);
                        }
                    }
                    requestParameters.Add(parameter.ParameterName, value);
                }                
            }
            return requestParameters;
        }
    }
}
