using System;
using System.Collections.Generic;

namespace Meridian
{
    public class RequestParameters : Dictionary<String,Object>
    {
        public RequestParameters()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}
