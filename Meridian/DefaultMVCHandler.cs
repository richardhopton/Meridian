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
            ViewEngineManager.CurrentEngine = viewEngine;
        }

        //Retrieve
        public void ProcessRequest(string url)
        {
            ProcessRequest(url, null, RequestVerbs.Retrieve);
        }

        //Submit
        public void ProcessRequest(string url, RequestParameters parameters)
        {
            ProcessRequest(url, parameters, RequestVerbs.Submit);
        }
        
        public void ProcessRequest(string url, RequestParameters parameters, string verb)
        {
            RouteData routeData = RouteTable.Routes.GetRouteData(url);

            if (routeData != null)
            {
                if (ContinueProcessing(url))
                {
                    string controllerName = routeData.GetRequiredString("Controller");
                    IController controller = _controllerFactory.CreateController(controllerName);
                    if (controller != null)
                    {
                        RequestContext context = new RequestContext(url, routeData, this, verb);
                        if (parameters != null)
                        {
                            foreach (var item in parameters)
                            {
                                context.RouteData.Values.Add(item.Key, item.Value);
                            }
                        }
                        controller.Execute(context);
                    }
                }
            }
        }

        private bool ContinueProcessing(string url)
        {
            if (_processing != null)
            {
                ProcessRequestEventArgs args = new ProcessRequestEventArgs(url, false);
                _processing(this, args);
                return !args.Cancel;
            }
            return true;
        }
    }
}
