using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Meridian.Routing
{
    public class RouteData
    {
        private RouteBase _route = null;

        public RouteBase Route
        {
            get { return _route; }
            set { _route = value; }
        }

        private RouteValueDictionary _values = new RouteValueDictionary();

        public RouteValueDictionary Values
        {
            get { return _values; }
            set { _values = value; }
        }

        public RouteData(RouteBase route)
        {
            Route = route;
        }

        public string GetRequiredString(string valueName)
        {
            Requires.NotNullOrEmpty(valueName, "valueName");

            object routeValue;
            if (this.Values.TryGetValue(valueName, out routeValue))
            {
                string str = routeValue as string;
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
            }
            throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, "Missing Required String: {0}", new object[] { valueName }));
        }
    }
}
