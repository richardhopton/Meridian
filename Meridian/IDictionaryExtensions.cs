using System;
using System.Collections.Generic;

namespace Meridian
{
    internal static class IDictionaryExtensions
    {
        internal static Object TryGetValue(this IDictionary<String, Object> defaults, String key)
        {
            if ((defaults != null) && (defaults.ContainsKey(key)))
            {
                return defaults[key];
            }
            return null;
        }
    }
}
