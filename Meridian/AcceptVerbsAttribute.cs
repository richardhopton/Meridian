using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Meridian
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=true)]
    public sealed class AcceptVerbsAttribute : Attribute
    {
        public IEnumerable<String> Verbs { get; private set; }

        public AcceptVerbsAttribute(params String[] verbs)
        {           
            Requires.NotNull(verbs, "verbs");

            Verbs = verbs.AsEnumerable<String>();
        }

        public static Boolean IsValidForRequest(ControllerContext context, MethodInfo method)
        {
            Requires.NotNull(context, "context");

            Object[] attr = method.GetCustomAttributes(typeof(AcceptVerbsAttribute), false);
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
