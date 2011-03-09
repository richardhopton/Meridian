namespace Meridian.SL
{
    public static class ControllerExtensions
    {
        public static IActionResult RedirectToAction(this Controller controller, string url)
        {
            return new RedirectToActionResult(url);
        }
    }
}
