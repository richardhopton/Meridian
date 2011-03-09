using System;

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
