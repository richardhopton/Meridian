using System.Windows.Controls;
using Meridian.SL.Navigation;

namespace Meridian.SL
{
    public class ViewPage : UserControl
    {
        private DelegateCommand<Request> _handleRequestCommand;

        public DelegateCommand<Request> HandleRequestCommand
        {
            get
            {
                if (_handleRequestCommand == null)
                {
                    _handleRequestCommand = new DelegateCommand<Request>(Handle, CanHandle);
                }
                return _handleRequestCommand;
            }
            set { _handleRequestCommand = value; }
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

        private bool CanHandle(object parameter)
        {
            return true;
        }

        private void Handle(Request request)
        {
            if (request != null)
            {
                string verb = string.IsNullOrEmpty(request.Verb) ? RequestVerbs.Submit : request.Verb;
                if (string.IsNullOrEmpty(request.Target))
                {
                    NavigationService.For(request.Target).Navigate(request.Url,
                                     request.Parameters.ToRequestParameters(this),
                                     verb);
                }
                else
                {
                    NavigationService.Default().Navigate(request.Url,
                                                         request.Parameters.ToRequestParameters(this),
                                                         verb );
                }
            }
        }

        public string Title
        {
            get { return JournalEntry.GetName(this); }
            set { JournalEntry.SetName(this, value); }
        }
    }
}