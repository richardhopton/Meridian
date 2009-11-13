using System.Windows;
using System.Windows.Controls;

namespace Meridian.SL
{
    public class ViewPage : UserControl
    {
        private DelegateCommand<string> _submitCommand;

        public DelegateCommand<string> SubmitCommand
        {
            get
            {
                if (_submitCommand == null)
                {
                    _submitCommand = new DelegateCommand<string>(Submit, CanSubmit);
                }
                return _submitCommand;
            }
            set { _submitCommand = value; }
        }

        public IMvcHandler Handler { get; set; }

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

        public ViewPage()
        {
            DataContext = this;
        }

        public bool CanSubmit(object parameter)
        {
            return true;
        }

        public void Submit(string url)
        {
            Handler.ProcessRequest(url, ViewData);
        }
    }
}