using System;

namespace Meridian.Routing
{
    public static class RouteTable
    {
        private static RouteCollection _routes = new RouteCollection();
        
        public static RouteCollection Routes
        {
            get { return _routes; }
        }
    }
}
