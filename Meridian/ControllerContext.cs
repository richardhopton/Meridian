using System;
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
            Requires.NotNull(controller, "controller");
            Requires.NotNull(requestContext, "requestContext");
            Requires.NotNull(routeData, "routeData");

            _controller = controller;
            _requestContext = requestContext;
            _routeData = routeData;
        }
    }
}
