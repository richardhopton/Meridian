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
    public class ActionResultEventArgs : EventArgs
    {
        public IActionResult ActionResult { get; set; }

        public ActionResultEventArgs(IActionResult actionResult)
        {
            ActionResult = actionResult;
        }
    }
}
