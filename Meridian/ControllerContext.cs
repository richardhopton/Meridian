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
    public class ControllerContext
    {
        private IController _controller = null;

        public IController Controller
        {
            get { return _controller; }
            set { _controller = value; }
        }

        private RequestContext _requestContext = null;

        public RequestContext RequestContext
        {
            get { return _requestContext; }
            set { _requestContext = value; }
        }

        private RouteData _routeData = null;

        public RouteData RouteData
        {
            get { return _routeData; }
            set { _routeData = value; }
        }

        public ControllerContext(IController controller, RequestContext requestContext, RouteData routeData)
        {
            _controller = controller;
            _requestContext = requestContext;
            _routeData = routeData;
        }
    }
}
