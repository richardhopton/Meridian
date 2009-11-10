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
using System.Reflection;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Meridian.Routing;

namespace Meridian
{
    public class DefaultActionInvoker : IActionInvoker
    {
        public void InvokeAction(ControllerContext context, string actionName)
        {
            Requires.NotNull(context, "context");
            Requires.NotNull(actionName, "actionName");

            MethodInfo method = GetSuitableMethod(context, actionName);
            if (method != null)
            {
                ParameterInfo[] parameters = method.GetParameters();

                object[] parameterValues = GetParameterValues(parameters, context.RouteData);

                IActionResult actionResult = method.Invoke(context.Controller, parameterValues) as IActionResult;

                if (actionResult != null)
                {
                    actionResult.Execute(context);
                }
            }
        }

        private MethodInfo GetSuitableMethod(ControllerContext context, string actionName)
        {
            IEnumerable<MethodInfo> methods = context.Controller.GetType().GetMethods().
                Where(m => m.Name.Equals(actionName, StringComparison.OrdinalIgnoreCase));

            //foreach (var method in methods)
            //{
            //    object[] attr = method.GetCustomAttributes(typeof(AcceptVerb), false);
            //    if (attr.Length > 0)
            //    {
            //        if ((attr[0] as AcceptVerb).Verb == context.RequestContext.Verb)
            //        {
            //            return method;
            //        }
            //    }
            //}
            return methods.FirstOrDefault();
        }

        private object[] GetParameterValues(ParameterInfo[] parameters, RouteData routeData)
        {
            Requires.NotNull(parameters, "parameters");
            Requires.NotNull(routeData, "routeData");
 
            if ((parameters.Length > 0)&&(routeData.Values.Count > 0))
        	{
                List<object> parameterValues = new List<object>();
                foreach (var parameter in parameters)
                {
                    object value = routeData.Values.TryGetValue(parameter.Name);
                    if (value != null)
                    {
                        try
                        {
                            value = Convert.ChangeType(value, parameter.ParameterType, Thread.CurrentThread.CurrentCulture);
                        }
                        catch
                        {
                        }
                    }
                    parameterValues.Add(value);
                }
                return parameterValues.ToArray();
	        }
            return null;
        }
    }
}
