using System;

namespace Meridian
{
    public class ViewResult : IActionResult
    {
        private ViewDataDictionary _viewData; 

        public ViewDataDictionary ViewData
        {
            get
            {
                if (_viewData == null)
                    _viewData = new ViewDataDictionary();
                return _viewData;
            }
            set { _viewData = value; }
        }

        public string ViewName { get; set; }

        public void Execute(ActionContext context)
        {
            Requires.NotNull(context, "context");

            if(String.IsNullOrEmpty(ViewName))
            {
                ViewName = context.ActionName;
            }
            var view = ViewEngineManager.CurrentEngine.GetView(context.ControllerContext, ViewName);
            if (view != null)
            {
                var viewContext = new ViewContext(context.ControllerContext, ViewData);                
                view.Render(viewContext);
            }
        }
    }
}
