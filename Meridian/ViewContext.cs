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
