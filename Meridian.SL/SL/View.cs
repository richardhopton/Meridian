using System;
using System.Windows;
using System.Windows.Controls;

namespace Meridian.SL
{
    public class View : IView
    {        
        public Type ViewType { get; set; }

        public void Render(ViewContext context)
        {
            Deployment.Current.Dispatcher.BeginInvoke(delegate()
              {
                  var page = Activator.CreateInstance(ViewType) as UserControl;
                  if (page is ViewPage)
                  {
                      var viewPage = page as ViewPage;
                      viewPage.Handler = context.RequestContext.Handler;
                      viewPage.ViewData = context.ViewData;
                  }
                  FrameManager.Display(context.RequestContext.Url, page);                                                          
              });
        }
    }
}