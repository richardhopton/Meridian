using System;
using System.Globalization;

namespace Meridian.Routing
{
    public class RouteData
    {
        public RouteBase Route { get; set; }

        public RouteValueDictionary Values { get; set; }

        public RouteData(RouteBase route)
        {
            Values = new RouteValueDictionary();
            Route = route;
        }

        public String GetString(String valueName)
        {
            Requires.NotNullOrEmpty(valueName, "valueName");

            Object routeValue;
            if (Values.TryGetValue(valueName, out routeValue))
            {
                var str = routeValue as String;
                if (!String.IsNullOrEmpty(str))
                {
                    return str;
                }
            }
            return null;
        }

        public String GetRequiredString(String valueName)
        {
            var value = GetString(valueName);
            if (!String.IsNullOrEmpty(value))
            {
                return value;
            }
            throw new InvalidOperationException(String.Format(CultureInfo.CurrentUICulture, "Missing Required String: {0}", new Object[] { valueName }));
        }
    }
}
