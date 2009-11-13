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
        private IMvcHandler _handler;

        public IMvcHandler Handler
        {
            get { return _handler; }
            set { _handler = value; }
        }

        private RouteData _routeData;

        public RouteData RouteData
        {
            get { return _routeData; }
            set { _routeData = value; }
        }

        private string _verb;

        public string Verb
        {
            get { return _verb; }
            set { _verb = value; }
        }

        public RequestContext(RouteData routeData, IMvcHandler handler, string verb)
        {
            _verb = verb;
            _handler = handler;
            _routeData = routeData;
        }
    }
}
