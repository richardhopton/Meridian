using System.Windows;
using System.Windows.Controls;

namespace Meridian.SL
{
    public class ViewPage : UserControl
    {
        public ViewDataDictionary ViewData { get; set; }

        public object Model
        {
            get
            {
                if (ViewData != null)
                {
                    return ViewData.Model;
                }
                return null;
            }
        }
    }
}