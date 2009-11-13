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
using System.Linq;
using System.Reflection;

namespace Meridian
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=true)]
    public sealed class AcceptVerbsAttribute : Attribute
    {
        public IEnumerable<string> Verbs { get; private set; }

        public AcceptVerbsAttribute(params string[] verbs)
        {           
            Requires.NotNull(verbs, "verbs");

            Verbs = verbs.AsEnumerable<string>();
        }

        public static bool IsValidForRequest(ControllerContext context, MethodInfo method)
        {
            Requires.NotNull(context, "context");

            object[] attr = method.GetCustomAttributes(typeof(AcceptVerbsAttribute), false);
            if (attr.Length > 0)
            {
                if((attr[0] as AcceptVerbsAttribute).Verbs.Contains(context.RequestContext.Verb, StringComparer.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
