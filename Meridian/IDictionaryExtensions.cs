using System.Collections.Generic;

namespace Meridian
{
    internal static class IDictionaryExtensions
    {
        internal static object TryGetValue(this IDictionary<string, object> defaults, string key)
        {
            if ((defaults != null) && (defaults.ContainsKey(key)))
            {
                return defaults[key];
            }
            return null;
        }
    }
}
