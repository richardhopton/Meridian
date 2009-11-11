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
using System.Linq.Expressions;

namespace Meridian.Routing
{
    public static class RouteCollectionExtensions
    {
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

        public static RouteBase MapRoute(this RouteCollection routes, string url)
        {
            return MapRoute(routes, null /* name */, url, null /* defaults */);
        }

        public static RouteBase MapRoute(this RouteCollection routes, string name, string url)
        {
            return MapRoute(routes, name, url, null /* defaults */);
        }

        public static RouteBase MapRoute(this RouteCollection routes, string url, params Expression<Func<String, String>>[] defaults)
        {
            return MapRoute(routes, null /* name */, url, defaults);
        }

        public static RouteBase MapRoute(this RouteCollection routes, string name, string url, params Expression<Func<String, String>>[] defaults)
        {
            Requires.NotNull(routes, "routes");
            Requires.NotNullOrEmpty(url,"url");

            RouteValueDictionary routeValueDefaults = null;
            if (defaults != null)
            {
                routeValueDefaults = new RouteValueDictionary();
                foreach (var expr in defaults)
                {
                    routeValueDefaults.Add(GetFuncParameter(expr), GetFuncResult(expr));
                }
            }

            Route route = new Route
                              {
                                  Url = url,
                                  Defaults = routeValueDefaults
                              };
            routes.Add(name, route);

            return route;
        }
    }
}
