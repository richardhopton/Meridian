using System;
using System.Collections.Generic;

namespace Meridian
{
    public static class IDictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey,TValue>(this IDictionary<TKey,TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            TValue resultValue;
            if (dict.TryGetValue(key, out resultValue))
            {
                return resultValue;
            }
            return defaultValue;
        }
    }
}
