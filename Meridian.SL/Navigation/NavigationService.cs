using System;
using Meridian.Routing;
using System.Windows;

namespace Meridian.SL.Navigation
{
    public class NavigationService
    {
        private static Journal _journal;
        private static IMvcHandler _mvcHandler;

        public static void RegisterHandler(IMvcHandler mvcHandler)
        {
            if (_mvcHandler != null)
                throw new InvalidOperationException("MvcHandler already set!");
            _mvcHandler = mvcHandler;
        }

        private readonly Frame _host;
        private Boolean _journalIsAddingHistoryPoint;

        public NavigationService(Frame host)
        {
            if (_mvcHandler == null)
            {
                throw new InvalidOperationException("MvcHandler not set!");
            }
            if(_journal == null)
            {
                _journal = new Journal(true);
            }
            _journal.Navigated += JournalNavigated;
            _host = host;
        }

        public static NavigationService For(string name)
        {
            Frame frame = UIHelper.FindViewFrame(Application.Current.RootVisual, name) as Frame;
            return new NavigationService(frame);
        }

        public static NavigationService For(Frame host)
        {
            Requires.NotNull(host, "host");
            return new NavigationService(host);
        }

        public static NavigationService Default()
        {
            Frame frame = UIHelper.FindViewFrame(Application.Current.RootVisual) as Frame;
            if (frame != null)
            {
                return new NavigationService(frame);
            }
            return null;
        }

        internal static Journal Journal
        {
            get { return _journal; }
        }

        public Boolean CanGoBack
        {
            get { return _journal.CanGoBack; }
        }

        public Boolean CanGoForward
        {
            get { return _journal.CanGoForward; }
        }

        public void Navigate(string url, RequestParameters parameters, String verb)
        {
            Requires.NotNull(url, "url");
            NavigateCore(url, parameters, verb, false);
        }

        public void Navigate(string url, RequestParameters parameters)
        {
            Navigate(url, parameters, RequestVerbs.Submit);
        }

        public void Navigate(string url)
        {
            Navigate(url, null, RequestVerbs.Retrieve);
        }

        internal Boolean ApplyDeepLinks()
        {
            return _journal.CheckForDeeplinks();
        }

        public void GoBack()
        {
            _journal.GoBack();
        }

        public void GoForward()
        {
            _journal.GoForward();
        }

        private void NavigateCore(String url, RequestParameters parameters,
                                  String verb, Boolean suppressJournalAdd)
        {
            FrameManager.Add(url, _host);
            _mvcHandler.ProcessRequest(url, parameters, verb);
            if (!suppressJournalAdd && (_host.Content != null))
            {
                String name = JournalEntry.GetName(_host.Content as ViewPage);
                try
                {
                    _journalIsAddingHistoryPoint = true;
                    _journal.AddHistoryPoint(new JournalEntry(name ?? url, url));
                }
                finally
                {
                    _journalIsAddingHistoryPoint = false;
                }
            }
        }

        private void JournalNavigated(object sender, JournalEventArgs e)
        {
            if (!_journalIsAddingHistoryPoint)
            {
                NavigateCore(e.Url, null, RequestVerbs.Retrieve, true);
            }
        }
    }
}