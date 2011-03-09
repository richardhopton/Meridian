using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Meridian.Routing
{
    public class RouteCollection : Collection<RouteBase>
    {
        private readonly Dictionary<String, RouteBase> _namedMap = new Dictionary<String, RouteBase>();

        private static String GetFuncParameter(Expression<Func<String, String>> func)
        {
            if (func != null)
            {
                return func.Parameters[0].Name;
            }
            return null;
        }

        private static String GetFuncResult(Expression<Func<String, String>> func)
        {
            if (func != null)
            {
                if (func.Body is ConstantExpression)
                {
                    return (String)(func.Body as ConstantExpression).Value;
                }
                return func.Compile().Invoke(null);
            }
            return null;
        }

        public RouteBase MapRoute(String url)
        {
            return MapRoute(null /* name */, url, null /* defaults */);
        }

        public RouteBase MapRoute(String name, String url)
        {
            return MapRoute(name, url, null /* defaults */);
        }

        public RouteBase MapRoute(String url, params Expression<Func<String, String>>[] defaults)
        {
            return MapRoute(null /* name */, url, defaults);
        }

        public RouteBase MapRoute(String name, String url, params Expression<Func<String, String>>[] defaults)
        {
            Requires.NotNullOrEmpty(url, "url");

            RouteValueDictionary routeValueDefaults = null;
            if (defaults != null)
            {
                routeValueDefaults = new RouteValueDictionary();
                foreach (var expr in defaults)
                {
                    routeValueDefaults.Add(GetFuncParameter(expr), GetFuncResult(expr));
                }
            }

            var route = new Route
            {
                Url = url,
                Defaults = routeValueDefaults
            };
            Add(name, route);

            return route;
        }
        
        public void Add(String name, RouteBase route)
        {
            Requires.NotNull(route, "route");

            Add(route);
            if (!String.IsNullOrEmpty(name))
            {
                _namedMap.Add(name, route);
            }
        }

        public RouteData GetRouteData(String url)
        {
            Requires.NotNull(url, "url");

            //using (this.GetReadLock())
            //{
            foreach (RouteBase route in this)
            {
                RouteData routeData = route.GetRouteData(url);
                if (routeData != null)
                {
                    return routeData;
                }
            }
            //}
            return null;
        }
    }
}
