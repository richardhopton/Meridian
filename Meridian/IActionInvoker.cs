using System;

namespace Meridian
{
    public interface IActionInvoker
    {
        void InvokeAction(ControllerContext context, String actionName);
    }
}
