using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Meridian.Routing
{
    public class RouteCollection : Collection<RouteBase>
    {
        private readonly Dictionary<string, RouteBase> _namedMap = new Dictionary<string, RouteBase>();

        public void Add(string name, RouteBase route)
        {
            Requires.NotNull(route, "route");

            Add(route);
            if (!string.IsNullOrEmpty(name))
            {
                _namedMap.Add(name, route);
            }
        }

        public RouteData GetRouteData(string url)
        {
            Requires.NotNullOrEmpty(url, "url");

  //          using (this.GetReadLock())
  //          {
            foreach (RouteBase route in this)
            {
                RouteData routeData = route.GetRouteData(url);
                if (routeData != null)
                {
                    return routeData;
                }
            }
  //        }
            return null;
        }
    }
}
