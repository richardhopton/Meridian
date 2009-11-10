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
    public class Controller : ControllerBase
    {
        public override void Execute()
        {
            string actionName = this.ControllerContext.RouteData.GetRequiredString("Action");
            
            this.ActionInvoker.InvokeAction(this.ControllerContext, actionName);
        }
    }
}
