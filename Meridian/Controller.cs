using System;

namespace Meridian
{
    public class Controller : ControllerBase
    {
        public override void Execute()
        {
            String actionName = ControllerContext.RouteData.GetRequiredString("Action");

            ActionInvoker.InvokeAction(ControllerContext, actionName);
        }

        protected internal ViewResult View()
        {
            return View(null /* viewName */, null /* model */);
        }

        protected internal ViewResult View(Object model)
        {
            return View(null /* viewName */, model);
        }

        protected internal ViewResult View(ViewDataDictionary viewData)
        {
            return ViewCore(null /* viewName */, viewData);
        }

        protected internal ViewResult View(String viewName)
        {
            return View(viewName, null /* model */);
        }

        protected internal ViewResult View(String viewName, Object model)
        {
            if (model != null)
            {
                ViewData.Model = model;
            }
            return ViewCore(viewName, ViewData);
        }
        
        private static ViewResult ViewCore(String viewName, ViewDataDictionary viewData)
        {
            return new ViewResult() {ViewData = viewData, ViewName = viewName};            
        }
    }
}
