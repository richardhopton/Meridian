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
    public class ViewContext
    {
        public ViewDataDictionary ViewData { get; set; }

        public IMvcHandler Handler { get; set; }

        public ViewContext(ViewDataDictionary viewData, IMvcHandler handler)
        {
            ViewData = viewData;
            Handler = handler;
        }
    }
}
