using Meridian.Routing;

namespace Meridian
{
    public class DefaultMvcHandler : IMvcHandler
    {
        private IControllerFactory _controllerFactory;

        public DefaultMvcHandler(IControllerFactory controllerFactory, IViewEngine viewEngine)
        {
            Requires.NotNull(controllerFactory, "controllerFactory");
            Requires.NotNull(viewEngine, "viewEngine");

            _controllerFactory = controllerFactory;
            ViewEngineManager.CurrentEngine = viewEngine;
        }

        //Retrieve
        public void ProcessRequest(string url)
        {            
            RouteData routeData = RouteTable.Routes.GetRouteData(url);

            if (routeData != null)
            {
                string controllerName = routeData.GetRequiredString("Controller");
                IController controller = _controllerFactory.CreateController(controllerName);
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
                IController controller = _controllerFactory.CreateController(controllerName);
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
