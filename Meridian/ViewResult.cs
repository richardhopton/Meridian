using System;

namespace Meridian
{
    public class ViewResult : IActionResult
    {
        public ViewDataDictionary ViewData { get; set; }
        public string ViewName { get; set; }

        public void Execute(ActionContext context)
        {
            Requires.NotNull(context, "context");
            if(String.IsNullOrEmpty(ViewName))
            {
                ViewName = context.ActionName;
            }
            IView view = ViewEngineManager.CurrentEngine.GetView(ViewName);
            if (view != null)
            {
                ViewContext viewContext = new ViewContext(context.ControllerContext, ViewData);                
                view.Render(viewContext);
            }
        }
    }
}
