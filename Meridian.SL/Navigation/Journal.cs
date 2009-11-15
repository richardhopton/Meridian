using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Interop;

namespace Meridian.SL.Navigation
{
    internal class Journal
    {
        private readonly Stack<JournalEntry> _backStack = new Stack<JournalEntry>();
        private readonly Stack<JournalEntry> _forwardStack = new Stack<JournalEntry>();
        
        private JournalEntry _currentEntry;
        private bool _suppressNavigationEvent;
        private EventHandler<NavigationStateChangedEventArgs> _weakRefEventHandler;

        internal Journal(bool useNavigationState)
        {
            UseNavigationState = useNavigationState;
            if (useNavigationState)
            {
                InitializeNavigationState();
            }
        }

        internal Stack<JournalEntry> BackStack
        {
            get { return _backStack; }
        }

        internal Stack<JournalEntry> ForwardStack
        {
            get { return _forwardStack; }
        }

        internal bool UseNavigationState { get; private set; }

        internal JournalEntry CurrentEntry
        {
            get { return _currentEntry; }
        }

        internal bool CanGoBack
        {
            get { return (_backStack.Count > 0); }
        }

        internal bool CanGoForward
        {
            get { return (_forwardStack.Count > 0); }
        }

        internal event EventHandler<JournalEventArgs> Navigated;

        ~Journal()
        {
            if (_weakRefEventHandler != null)
            {
                Action a = delegate { Application.Current.Host.NavigationStateChanged -= _weakRefEventHandler; };
                Deployment.Current.Dispatcher.BeginInvoke(a);
            }
        }

        private void Browser_Navigated(String url)
        {
            if (UseNavigationState)
            {
                AddHistoryPointIfDifferent(url);
            }
        }

        internal bool CheckForDeeplinks()
        {
            if (UseNavigationState)
            {
                string str = Application.Current.Host.NavigationState;
                if (!string.IsNullOrEmpty(str))
                {
                    AddHistoryPointIfDifferent(str);
                    return true;
                }
            }
            return false;
        }

        private void AddHistoryPointIfDifferent(string newState)
        {
            string b = string.Empty;
            if (CurrentEntry != null)
            {
                b = CurrentEntry.Url;
            }
            if (!string.Equals(newState, b, StringComparison.Ordinal))
            {
                _suppressNavigationEvent = true;
                AddHistoryPoint(new JournalEntry(String.Empty, newState));
                _suppressNavigationEvent = false;
            }
        }

        internal void AddHistoryPoint(JournalEntry journalEntry)
        {
            Requires.NotNull(journalEntry, "journalEntry");
            _forwardStack.Clear();
            if (_currentEntry != null)
            {
                _backStack.Push(_currentEntry);
            }
            _currentEntry = journalEntry;
            UpdateObservables(_currentEntry, NavigationMode.New);
            UpdateNavigationState(_currentEntry);
        }

        internal void GoBack()
        {
            if (!CanGoBack)
            {
                throw new InvalidOperationException("Cannot GoBack");
            }
            _forwardStack.Push(_currentEntry);
            _currentEntry = _backStack.Pop();
            UpdateObservables(_currentEntry, NavigationMode.Back);
            UpdateNavigationState(_currentEntry);
        }

        internal void GoForward()
        {
            if (!CanGoForward)
            {
                throw new InvalidOperationException("Cannot GoForward");
            }
            _backStack.Push(_currentEntry);
            _currentEntry = _forwardStack.Pop();

            UpdateObservables(_currentEntry, NavigationMode.Forward);
            UpdateNavigationState(_currentEntry);
        }

        private void InitializeNavigationState()
        {
            var thisWeak = new WeakReference(this);
            _weakRefEventHandler = delegate(object sender, NavigationStateChangedEventArgs args)
                                       {
                                           var target = thisWeak.Target as Journal;
                                           if (target != null)
                                           {
                                               target.Browser_Navigated(args.NewNavigationState);
                                           }
                                       };
            try
            {
                Application.Current.Host.NavigationStateChanged += _weakRefEventHandler;
            }
            catch (InvalidOperationException)
            {
                UseNavigationState = false;
            }
        }

        protected void OnNavigated(String name, String url, NavigationMode mode)
        {
            EventHandler<JournalEventArgs> navigated = Navigated;
            if (navigated != null)
            {
                var e = new JournalEventArgs(name, url, mode);
                navigated(this, e);
            }
        }

        private void UpdateObservables(JournalEntry journalEntry, NavigationMode mode)
        {
            OnNavigated(journalEntry.Name, journalEntry.Url, mode);
        }

        private void UpdateNavigationState(JournalEntry journalEntry)
        {
            if (UseNavigationState && !_suppressNavigationEvent)
            {
                Application.Current.Host.NavigationState = journalEntry.Url;
                if (HtmlPage.IsEnabled)
                {
                    HtmlPage.Document.SetProperty("title", journalEntry.Name);
                }
            }
        }
    }
}