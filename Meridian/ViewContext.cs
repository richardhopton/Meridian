using System;

namespace Meridian
{
    public class ViewContext : ControllerContext
    {
        public ViewDataDictionary ViewData { get; set; }

        public ViewContext(ControllerContext context, ViewDataDictionary viewData) : 
            base(context.Controller, context.RequestContext, context.RouteData)
        {
            Requires.NotNull(context, "context");
            Requires.NotNull(viewData, "viewData");

            ViewData = viewData;
        }
    }
}
