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

namespace Meridian.SL
{
    public class RedirectToActionResult : IActionResult
    {
        public string Url { get; set; }

        public void Execute(ActionContext context)
        {
            //Possibly need to think about how we handle a redirect to action with multiple frames
            Navigation.NavigationService.Default().Navigate(Url);
        }
    }
}
