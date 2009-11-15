using System;
using System.Windows;
using System.Windows.Browser;

namespace Meridian.SL.Navigation
{
    internal sealed class JournalEntry : DependencyObject
    {
        // Fields

        public static readonly DependencyProperty NameProperty = DependencyProperty.RegisterAttached("Name",
                                                                                                     typeof (string),
                                                                                                     typeof (
                                                                                                         JournalEntry),
                                                                                                     new PropertyMetadata
                                                                                                         (new PropertyChangedCallback
                                                                                                              (NamePropertyChanged)));

        private String _url;

        // Methods
        public JournalEntry(string name, String url)
        {
            Requires.NotNull(url, "journalEntry");
            Name = name;
            _url = url;
        }

        public string Name
        {
            get { return (string) GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public String Url
        {
            get { return _url; }
            set
            {
                Requires.NotNull(value, "value");
                _url = value;
            }
        }

        public static string GetName(DependencyObject obj)
        {
            Requires.NotNull(obj, "obj");
            return (string) obj.GetValue(NameProperty);
        }

        private static void NamePropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var page = depObj as ViewPage;
            if ((page != null) &&
                (NavigationService.Journal != null) &&
                NavigationService.Journal.UseNavigationState &&
                HtmlPage.IsEnabled)
            {
                HtmlPage.Document.SetProperty("title", e.NewValue);
            }
        }

        public static void SetName(DependencyObject obj, string name)
        {
            Requires.NotNull(obj, "obj");
            obj.SetValue(NameProperty, name);
        }

        // Properties
    }
}