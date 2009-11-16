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
using Meridian.SL.Navigation;

namespace Meridian.SL
{
    public interface IFrame
    {
        void Display(UIElement element);
        String GetContentTitle();
        NavigationService NavigationService { get; }
    }
}
