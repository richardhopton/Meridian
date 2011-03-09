using System;

namespace Meridian.SL.Navigation
{
    public class NavigationEventArgs : EventArgs
    {
        public Uri Uri { get; set; }

        public NavigationEventArgs(Uri uri)
        {
            Uri = uri;
        }
    }
}
