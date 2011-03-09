using System;
using System.Windows;
using Meridian.SL.Navigation;

namespace Meridian.SL
{
    public interface IFrame
    {
        void Display(String url, UIElement element);
        String GetContentTitle();
        NavigationService NavigationService { get; }
        event EventHandler<NavigationEventArgs> Navigated;
    }
}
