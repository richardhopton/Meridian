using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Meridian.Routing;

namespace Meridian
{
	public class DefaultActionInvoker : IActionInvoker
	{
		private readonly Dictionary<IAsyncController, ActionContext> _contextCache = new Dictionary<IAsyncController, ActionContext>();

		public void InvokeAction(ControllerContext context, String actionName)
		{
			Requires.NotNull(context, "context");
			Requires.NotNull(actionName, "actionName");

			MethodInfo method = GetSuitableMethod(context, actionName);
			if (method != null)
			{
				ParameterInfo[] parameters = method.GetParameters();

				Object[] parameterValues = GetParameterValues(parameters, context.RouteData);

				var actionContext = new ActionContext(context, actionName);
				IAsyncController asyncController = context.Controller as IAsyncController;
				if ((asyncController != null)&&(method.ReturnType.Equals(typeof(void))))
				{
					asyncController.ActionCompleted += DefaultActionInvoker_ActionCompleted;
					_contextCache.Add(asyncController, actionContext);
					method.Invoke(context.Controller, parameterValues);
				}
				else
				{
					IActionResult actionResult = method.Invoke(context.Controller, parameterValues) as IActionResult;
					if (actionResult != null)
					{
						actionResult.Execute(actionContext);
					}
				}
			}
		}

		private void DefaultActionInvoker_ActionCompleted(Object sender, ActionResultEventArgs e)
		{
			IAsyncController controller = sender as IAsyncController;
			if (controller != null)
			{
				controller.ActionCompleted -= DefaultActionInvoker_ActionCompleted;            
				if (e.ActionResult != null)
				{
					ActionContext context = null;
					if (_contextCache.TryGetValue(controller, out context))
					{
						_contextCache.Remove(controller);
						e.ActionResult.Execute(context);
					}
				}
			}
		}

		private MethodInfo GetSuitableMethod(ControllerContext context, String actionName)
		{
			IEnumerable<MethodInfo> methods = context.Controller.GetType().GetMethods().
				Where(m => m.Name.Equals(actionName, StringComparison.OrdinalIgnoreCase));

			foreach (var method in methods)
			{
				if (AcceptVerbsAttribute.IsValidForRequest(context, method))
				{
					return method;
				}
			}
			return methods.FirstOrDefault();
		}        

		private Object[] GetParameterValues(ParameterInfo[] parameters, RouteData routeData)
		{
			Requires.NotNull(parameters, "parameters");
			Requires.NotNull(routeData, "routeData");
 
			if ((parameters.Length > 0)&&(routeData.Values.Count > 0))
			{
				List<Object> parameterValues = new List<Object>();
				foreach (var parameter in parameters)
				{
					Object value = routeData.Values.GetValueOrDefault(parameter.Name);
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
