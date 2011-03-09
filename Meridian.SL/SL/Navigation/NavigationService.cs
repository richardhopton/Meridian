using System;
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

        private readonly IFrame _host;
        private Boolean _journalIsAddingHistoryPoint;

        public NavigationService(IFrame host)
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

        public static NavigationService For(String name)
        {
            IFrame frame = UIHelper.FindViewFrame(Application.Current.RootVisual, name) as Frame;
            if (frame != null)
            {
                return frame.NavigationService;
            }
            else
            {
                return NavigationService.Default;
            }
        }

        public static NavigationService For(IFrame host)
        {
            Requires.NotNull(host, "host");
            return host.NavigationService;
        }

        public static NavigationService Default
        {
            get
            {
                IFrame frame = UIHelper.FindViewFrame(Application.Current.RootVisual) as Frame;
                if (frame != null)
                {
                    return frame.NavigationService;
                }
                return null;
            }
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

        public void Navigate(Request request)
        {
            Requires.NotNull(request, "request");
            Navigate(request.Url, request.Parameters.ToRequestParameters(), request.Verb);
        }

        public void Navigate(String url, RequestParameters parameters, String verb)
        {
            Requires.NotNull(url, "url");
            NavigateCore(url, parameters, verb, false);
        }

        public void Navigate(String url, RequestParameters parameters)
        {
            Navigate(url, parameters, RequestVerbs.Submit);
        }

        public void Navigate(String url)
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
            if (!suppressJournalAdd)
            {
                String name = _host.GetContentTitle();
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

        private void JournalNavigated(Object sender, JournalEventArgs e)
        {
            if (!_journalIsAddingHistoryPoint)
            {
                NavigateCore(e.Url, null, RequestVerbs.Retrieve, true);
            }
        }
    }
}