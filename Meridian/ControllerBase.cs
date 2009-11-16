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
    public abstract class ControllerBase : IController
    {
        private ControllerContext _controllerContext = null;

        public ControllerContext ControllerContext
        {
            get { return _controllerContext; }
            set { _controllerContext = value; }
        }

        private IActionInvoker _actionInvoker = null;

        public IActionInvoker ActionInvoker
        {
            get { return _actionInvoker; }
            set { _actionInvoker = value; }
        }

        private ViewDataDictionary _viewData;

        public ViewDataDictionary ViewData
        {
            get
            {
                if (_viewData == null)
                {
                    _viewData = new ViewDataDictionary();
                }
                return _viewData; 
            }
            set { _viewData = value; }
        }

        public ControllerBase()
        {
            _actionInvoker = new DefaultActionInvoker();
        }
        
        public void Execute(RequestContext context)
        {
            _controllerContext = new ControllerContext(this, context, context.RouteData);
            Execute();
        }

        public abstract void Execute();      
    }
}
