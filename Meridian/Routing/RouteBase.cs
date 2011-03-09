using System;

namespace Meridian.Routing
{
    public abstract class RouteBase
    {
        public abstract RouteData GetRouteData(String url);
    }
}
