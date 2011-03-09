using System;
using System.Windows;
using System.Windows.Media;

namespace Meridian.SL
{
    public static class UIHelper
    {
        public static IFrame FindViewFrame(DependencyObject parent)
        {
            return FindViewFrame(parent, null /* name */ );
        }        

        public static IFrame FindViewFrame(DependencyObject parent, String name)
        {
            for (Int32 i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
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

        private static Boolean IsIFrame(DependencyObject element, String name)
        {
            if (typeof(IFrame).IsAssignableFrom(element.GetType()))
            {
                if (!String.IsNullOrEmpty(name)
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
