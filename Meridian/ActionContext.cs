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
    public class ActionContext
    {
        private ControllerContext _controllerContext = null;
        public ControllerContext ControllerContext
        {
            get { return _controllerContext; }
            set { _controllerContext = value; }
        }

        private string _actionName = String.Empty;
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
