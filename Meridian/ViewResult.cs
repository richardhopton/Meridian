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
    public class ViewResult : IActionResult
    {
        public ViewDataDictionary ViewData { get; set; }
        public string ViewName { get; set; }

        public void Execute(ControllerContext context)
        {
            Requires.NotNull(context, "context");

            IViewEngine viewEngine = null; //ToDo: Implement Get View Engine Logic
            IView view = viewEngine.GetView(ViewName);

            ViewContext viewContext = new ViewContext();
            viewContext.ViewData = ViewData;

            view.Render(viewContext);
        }
    }
}
