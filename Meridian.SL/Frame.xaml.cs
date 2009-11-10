using System.Windows;
using System.Windows.Controls;

namespace Meridian.SL
{
    public partial class Frame : UserControl, IFrame
    {
        public Frame()
        {
            InitializeComponent();
        }

        public void Display(UIElement element)
        {
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(element);
        }
    }
}
