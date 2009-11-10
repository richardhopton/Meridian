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
using Meridian.Routing;

namespace Meridian
{
    public class RequestContext
    {
        private RouteData _routeData = null;

        public RouteData RouteData
        {
            get { return _routeData; }
            set { _routeData = value; }
        }

        public RequestContext(RouteData routeData)
        {
            _routeData = routeData;
        }
    }
}
