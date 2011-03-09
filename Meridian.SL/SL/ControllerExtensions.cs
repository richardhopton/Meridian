using System;

namespace Meridian.SL
{
    public static class ControllerExtensions
    {
        public static IActionResult RedirectToAction(this Controller controller, String url)
        {
            return new RedirectToActionResult(url);
        }
    }
}
