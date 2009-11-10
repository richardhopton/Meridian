using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Meridian
{
    public static class IDictionaryExtensions
    {
        public static object TryGetValue(this IDictionary<string, object> defaults, string key)
        {
            if ((defaults != null) && (defaults.ContainsKey(key)))
            {
                return defaults[key];
            }
            return null;
        }
    }
}
