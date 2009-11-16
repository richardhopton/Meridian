using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Meridian.SL
{
    public class View : IView
    {        
        public Type ViewType { get; set; }

        public void Render(ViewContext context)
        {
            UserControl page = Activator.CreateInstance(ViewType) as UserControl;
            if (page is ViewPage)
            {
                var viewPage = page as ViewPage;
                viewPage.Handler = context.RequestContext.Handler;
                viewPage.ViewData = context.ViewData;
                viewPage.DataContext = viewPage;
            }
            FrameManager.Display(context.RequestContext.Url, page);
        }
    }
}