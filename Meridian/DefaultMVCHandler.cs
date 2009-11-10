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
using Meridian.Routing;

namespace Meridian
{
    public class DefaultMVCHandler : IMVCHandler
    {
        //Retrieve
        public void ProcessRequest(string url)
        {            
            RouteData routeData = RouteTable.Routes.GetRouteData(url);

            if (routeData != null)
            {
                string controllerName = routeData.GetRequiredString("Controller");

                IControllerFactory factory = new DefaultControllerFactory();
                IController controller = factory.CreateController(controllerName);

                RequestContext context = new RequestContext(routeData);

                controller.Execute(context);
            }
        }

        //Submit
        public void ProcessRequest(string url, ViewDataDictionary viewData)
        {
            RouteData routeData = RouteTable.Routes.GetRouteData(url);

            if (routeData != null)
            {
                string controllerName = routeData.GetRequiredString("Controller");

                IControllerFactory factory = new DefaultControllerFactory();
                IController controller = factory.CreateController(controllerName);

                RequestContext context = new RequestContext(routeData);

                if (viewData != null)
                {
                    foreach (var item in viewData)
                    {
                        context.RouteData.Values.Add(item.Key, item.Value);
                    }
                }

                controller.Execute(context);
            }
        }
    }
}
