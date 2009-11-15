using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System;
using Meridian.SL.Navigation;

namespace Meridian.SL
{
    public class Frame : ContentControl, IFrame, INavigate
    {
        private NavigationService _navigationService;

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

        public static readonly DependencyProperty InitialSourceProperty = DependencyProperty.Register("InitialSource", typeof(String), typeof(Frame), new PropertyMetadata(null));

        public String InitialSource
        {
            get
            {
                return (GetValue(InitialSourceProperty) as String);
            }
            set
            {
                SetValue(InitialSourceProperty, value);
            }
        }

        private Boolean _loaded;
        private String _deferredNavigation;

        void Frame_Loaded(object sender, RoutedEventArgs e)
        {
            this._loaded = true;
            if (!IsInDesignMode())
            {
                if (!this.ApplyDeepLinks())
                {
                    if (_deferredNavigation != null)
                    {
                        this.Navigate(_deferredNavigation);
                        _deferredNavigation = null;
                    }
                    else if (this.InitialSource != null)
                    {
                        Navigate(this.InitialSource);
                    }
                    else
                    {
                        Navigate(String.Empty);
                    }
                }
            }
        }

        public void Display(UIElement element)
        {
            this.Content = null;
            this.Content = element;
        }

        internal bool ApplyDeepLinks()
        {
            return this._navigationService.ApplyDeepLinks();
        }

        private Boolean Navigate(String url)
        {
            if (this._loaded)
            {
                this._navigationService.Navigate(url);
            }
            this._deferredNavigation = url;
            return true;
        }

        #region INavigate Members

        public bool Navigate(Uri source)
        {
            return this.Navigate(source.OriginalString);
        }

        #endregion
    }
}
