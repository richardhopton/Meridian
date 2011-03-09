using System;
using Meridian.Routing;

namespace Meridian
{
    public class RequestContext
    {
        private String _url;

        public String Url
        {
            get { return _url; }
            set { _url = value; }
        }

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

        private String _verb;

        public String Verb
        {
            get { return _verb; }
            set { _verb = value; }
        }

        public RequestContext(String url, RouteData routeData, IMvcHandler handler, String verb)
        {
            _url = url;
            _verb = verb;
            _handler = handler;
            _routeData = routeData;
        }
    }
}
