using System;
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

        public ViewPage()
        {
            Loaded += ViewPage_Loaded;
        }

        void ViewPage_Loaded(Object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = this;
        }

        public Object Model
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

        protected Boolean CanHandle(Object parameter)
        {
            return true;
        }

        protected void Handle(Request request)
        {
            if (request != null)
            {
                String verb = String.IsNullOrEmpty(request.Verb) ? RequestVerbs.Submit : request.Verb;
                if (!String.IsNullOrEmpty(request.Target))
                {
                    NavigationService.For(request.Target).Navigate(request.Url,
                                     request.Parameters.ToRequestParameters(this),
                                     verb);
                }
                else
                {
                    NavigationService.Default.Navigate(request.Url,
                                                         request.Parameters.ToRequestParameters(this),
                                                         verb );
                }
            }
        }

        public String Title
        {
            get { return JournalEntry.GetName(this); }
            set { JournalEntry.SetName(this, value); }
        }
    }
}