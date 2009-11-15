using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Meridian.SL.Navigation;

namespace Meridian.SL
{
    public class Frame : ContentControl, IFrame, INavigate
    {
        private readonly NavigationService _navigationService;

        public Frame()
        {
            Loaded += Frame_Loaded;
            _navigationService = new NavigationService(this);
        }

        internal static bool IsInDesignMode()
        {
            if ((Application.Current != null) && (Application.Current.GetType() != typeof(Application)))
            {
                return DesignerProperties.IsInDesignTool;
            }
            return true;
        }

        public static readonly DependencyProperty InitialSourceProperty =
            DependencyProperty.Register("InitialSource",
                                        typeof(String),
                                        typeof(Frame),
                                        new PropertyMetadata(null));

        public String InitialSource
        {
            get { return (GetValue(InitialSourceProperty) as String); }
            set { SetValue(InitialSourceProperty, value); }
        }

        private Boolean _loaded;
        private String _deferredNavigation;

        private void Frame_Loaded(object sender, RoutedEventArgs e)
        {
            _loaded = true;
            if (!IsInDesignMode())
            {
                if (!_navigationService.ApplyDeepLinks())
                {
                    if (_deferredNavigation != null)
                    {
                        Navigate(_deferredNavigation);
                        _deferredNavigation = null;
                    }
                    else if (InitialSource != null)
                    {
                        Navigate(InitialSource);
                    }
                    else
                    {
                        Navigate(String.Empty);
                    }
                }
            }
        }

        #region IFrame Members

        public void Display(UIElement element)
        {
            Content = null;
            Content = element;
        }

        #endregion

        private Boolean Navigate(String url)
        {
            if (_loaded)
            {
                _navigationService.Navigate(url);
            }
            _deferredNavigation = url;
            return true;
        }

        #region INavigate Members

        public bool Navigate(Uri source)
        {
            return Navigate(source.OriginalString);
        }

        #endregion
    }
}