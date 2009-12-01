namespace Meridian
{
    public class Controller : ControllerBase
    {
        public override void Execute()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("Action");

            ActionInvoker.InvokeAction(ControllerContext, actionName);
        }

        protected internal ViewResult View()
        {
            return View(null /* viewName */, null /* model */);
        }

        protected internal ViewResult View(object model)
        {
            return View(null /* viewName */, model);
        }

        protected internal ViewResult View(ViewDataDictionary viewData)
        {
            return ViewCore(null /* viewName */, viewData);
        }

        protected internal ViewResult View(string viewName)
        {
            return View(viewName, null /* model */);
        }

        protected internal ViewResult View(string viewName, object model)
        {
            if (model != null)
            {
                ViewData.Model = model;
            }
            return ViewCore(viewName, ViewData);
        }
        
        private static ViewResult ViewCore(string viewName, ViewDataDictionary viewData)
        {
            return new ViewResult() {ViewData = viewData, ViewName = viewName};            
        }
    }
}
