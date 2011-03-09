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
    public static class UIHelper
    {
        public static IFrame FindViewFrame(DependencyObject parent)
        {
            return FindViewFrame(parent, null /* name */ );
        }        

        public static IFrame FindViewFrame(DependencyObject parent, string name)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (typeof(IFrame).IsAssignableFrom(child.GetType()))
                {
                    return child as IFrame;
                }
                IFrame result = FindViewFrame(child, name);
                if (result != null)
                    return result;
            }
            return null;
        }

        private static bool IsIFrame(DependencyObject element, string name)
        {
            if (typeof(IFrame).IsAssignableFrom(element.GetType()))
            {
                if (!string.IsNullOrEmpty(name)
                    && ((element is FrameworkElement)
                    &&((FrameworkElement)element).Name.Equals(name)))
                {
                    return true;
                }
                return true;
            }
            return false;
        }
    }
}
