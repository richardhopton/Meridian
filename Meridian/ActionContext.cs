using System;

namespace Meridian
{
    public class ActionContext
    {
        private ControllerContext _controllerContext = null;
        public ControllerContext ControllerContext
        {
            get { return _controllerContext; }
            set { _controllerContext = value; }
        }

        private String _actionName = String.Empty;
        public String ActionName
        {
            get { return _actionName; }
            set { _actionName = value; }
        }

        public ActionContext(ControllerContext controllerContext, String actionName)
        {
            _controllerContext = controllerContext;
            _actionName = actionName;
        }
    }
}
