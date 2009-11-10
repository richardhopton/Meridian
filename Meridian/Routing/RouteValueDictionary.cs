using System;
using System.Collections.Generic;

namespace Meridian.Routing
{
    public class RouteValueDictionary : Dictionary<string,object>
    {
        public RouteValueDictionary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}
