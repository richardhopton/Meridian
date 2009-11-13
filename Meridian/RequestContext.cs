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
        private IMvcHandler _handler = null;

        public IMvcHandler Handler
        {
            get { return _handler; }
            set { _handler = value; }
        }

        private RouteData _routeData = null;

        public RouteData RouteData
        {
            get { return _routeData; }
            set { _routeData = value; }
        }

        public RequestContext(RouteData routeData, IMvcHandler handler)
        {
            _handler = handler;
            _routeData = routeData;
        }
    }
}
