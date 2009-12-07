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
using System.Collections.Generic;

namespace Meridian
{
    public class ViewEngineManager
    {
        private readonly List<IViewEngine> _viewEngines = new List<IViewEngine>();
        private static ViewEngineManager _instance;

        public void Add(IViewEngine viewEngine)
        {
            _viewEngines.Add(viewEngine);
        }

        public static ViewEngineManager Current
        {
            get
            {
                if (_instance == null)
                    _instance = new ViewEngineManager();
                return _instance;
            }
        }

        public IView GetView(ControllerContext context, string viewName)
        {
            IView view = null;
            foreach (var viewEngine in _viewEngines)
            {
                view = viewEngine.GetView(context, viewName);
                if (view != null)
                {
                    break;
                }
            }
            return view;
        }

        private ViewEngineManager()
        {           
        }
    }
}
