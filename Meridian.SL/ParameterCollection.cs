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

namespace Meridian.SL
{
    public class ParameterCollection : ObservableCollection<Parameter>
    {
        public RequestParameters ToRequestParameters()
        {
            RequestParameters requestParameters = new RequestParameters();
            foreach (var parameter in this)
            {
                requestParameters.Add(parameter.ParameterName, parameter.Value);
            }
            return requestParameters;
        }
    }
}
