using System;
using System.Collections.Generic;

namespace Meridian.Routing
{
    public class RouteValueDictionary : Dictionary<String,Object>
    {
        public RouteValueDictionary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}
