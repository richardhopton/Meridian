﻿using System;
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

        public string GetRequiredString(string valueName)
        {
            Requires.NotNullOrEmpty(valueName, "valueName");

            object routeValue;
            if (Values.TryGetValue(valueName, out routeValue))
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