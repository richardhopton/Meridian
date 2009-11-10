using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meridian.Routing
{
    public abstract class RouteBase
    {
        public abstract RouteData GetRouteData(string url);
    }
}
