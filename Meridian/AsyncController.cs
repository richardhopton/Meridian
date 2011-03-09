using System;

namespace Meridian
{
    public class AsyncController : Controller, IAsyncController
    {
        protected void CompleteAction(IActionResult result)
        {
            if (ActionCompleted != null)
            {
                ActionCompleted(this, new ActionResultEventArgs(result));
            }
        }

        public event EventHandler<ActionResultEventArgs> ActionCompleted;
    }
}
