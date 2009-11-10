namespace Meridian
{
    public class ViewResult : IActionResult
    {
        public ViewDataDictionary ViewData { get; set; }
        public string ViewName { get; set; }

        public void Execute(ControllerContext context)
        {
            Requires.NotNull(context, "context");
            
            IView view = ViewEngineManager.CurrentEngine.GetView(ViewName);
            if (view != null)
            {
                ViewContext viewContext = new ViewContext {ViewData = ViewData};
                view.Render(viewContext);
            }
        }
    }
}
