using System;

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

        public void Navigate(string url, ViewDataDictionary viewData, String verb)
        {
            Requires.NotNull(url, "journalEntry");
            NavigateCore(url, viewData, verb, false);
        }

        public void Navigate(string url, ViewDataDictionary viewData)
        {
            Navigate(url, viewData, RequestVerbs.Submit);
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

        private void NavigateCore(String url, ViewDataDictionary viewData,
                                  String verb, Boolean suppressJournalAdd)
        {
            _mvcHandler.ProcessRequest(url, viewData, verb);
            if (!suppressJournalAdd)
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