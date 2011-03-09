using System;
using System.Collections.Generic;

namespace Meridian
{
    public class ViewDataDictionary : Dictionary<String,Object>
    {
        public Object Model { get; set; }

        public ViewDataDictionary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}
