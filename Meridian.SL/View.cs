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
            IFrame target = FindViewFrame(Application.Current.RootVisual);
            if (target != null)
            {
                UserControl page = Activator.CreateInstance(ViewType) as UserControl;
                if (page is ViewPage)
                {
                    ((ViewPage) page).ViewData = context.ViewData;
                    ((ViewPage)page).Handler = context.Handler;
                }
                target.Display(page);
            }
        }

        private static IFrame FindViewFrame(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (typeof (IFrame).IsAssignableFrom(child.GetType()))
                {
                    return child as IFrame;
                }
                IFrame result = FindViewFrame(child);
                if (result != null)
                    return result;
            }
            return null;
        }
    }
}