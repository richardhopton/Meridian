using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
