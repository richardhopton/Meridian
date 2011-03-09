using System;
using Meridian.Routing;

namespace Meridian
{
    public class DefaultMvcHandler : IMvcHandler
    {
        private IControllerFactory _controllerFactory;

        private event ProcessRequestEventHandler _processing;

        public event ProcessRequestEventHandler Processing
        {
            add { _processing += value; }
            remove { _processing -= value; }
        }

        public DefaultMvcHandler(IControllerFactory controllerFactory, IViewEngine viewEngine)
        {
            Requires.NotNull(controllerFactory, "controllerFactory");
            Requires.NotNull(viewEngine, "viewEngine");

            _controllerFactory = controllerFactory;
            ViewEngineManager.Current.Add(viewEngine);
        }

        //Retrieve
        public void ProcessRequest(String url)
        {
            ProcessRequest(url, null, RequestVerbs.Retrieve);
        }

        //Submit
        public void ProcessRequest(String url, RequestParameters parameters)
        {
            ProcessRequest(url, parameters, RequestVerbs.Submit);
        }
        
        public void ProcessRequest(String url, RequestParameters parameters, String verb)
        {
            RouteData routeData = RouteTable.Routes.GetRouteData(url);

            if (routeData != null)
            {
                if (ContinueProcessing(url, verb))
                {
                    String controllerName = routeData.GetRequiredString("Controller");
                    IController controller = _controllerFactory.CreateController(controllerName);
                    if (controller != null)
                    {
                        var context = new RequestContext(url, routeData, this, verb);
                        if (parameters != null)
                        {
                            foreach (var item in parameters)
                            {
                                if (context.RouteData.Values.ContainsKey(item.Key))
                                    context.RouteData.Values[item.Key] = item.Value;
                                else
                                    context.RouteData.Values.Add(item.Key, item.Value);
                            }
                        }
                        controller.Execute(context);
                    }
                }
            }
        }

        private Boolean ContinueProcessing(String url, String verb)
        {
            if (_processing != null)
            {
                var args = new ProcessRequestEventArgs(url, false, verb);
                _processing(this, args);
                return !args.Cancel;
            }
            return true;
        }
    }
}
