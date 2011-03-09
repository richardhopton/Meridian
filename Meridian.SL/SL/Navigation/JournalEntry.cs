using System;
using System.Windows;
using System.Windows.Browser;

namespace Meridian.SL.Navigation
{
    internal sealed class JournalEntry : DependencyObject
    {
        // Fields

        public static readonly DependencyProperty NameProperty = DependencyProperty.RegisterAttached("Name", typeof(String), typeof(JournalEntry), new PropertyMetadata(new PropertyChangedCallback(NamePropertyChanged)));

        private String _url;

        // Methods
        public JournalEntry(String name, String url)
        {
            Requires.NotNull(url, "journalEntry");
            Name = name;
            _url = url;
        }

        public String Name
        {
            get { return (String) GetValue(NameProperty); }
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

        public static String GetName(DependencyObject obj)
        {
            Requires.NotNull(obj, "obj");
            return (String) obj.GetValue(NameProperty);
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

        public static void SetName(DependencyObject obj, String name)
        {
            Requires.NotNull(obj, "obj");
            obj.SetValue(NameProperty, name);
        }

        // Properties
    }
}