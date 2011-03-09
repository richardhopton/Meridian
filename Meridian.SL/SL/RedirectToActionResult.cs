using System;

namespace Meridian.SL
{
    public class RedirectToActionResult : IActionResult
    {
        public String Url { get; set; }

        public RedirectToActionResult(String url)
        {
            Url = url;
        }

        public void Execute(ActionContext context)
        {
            //ToDo: need to think about how we handle a redirect to action with multiple frames
            Navigation.NavigationService.Default.Navigate(Url);
        }
    }
}
