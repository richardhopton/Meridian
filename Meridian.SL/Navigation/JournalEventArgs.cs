using System;

namespace Meridian.SL.Navigation
{
    internal sealed class JournalEventArgs : EventArgs
    {
        internal JournalEventArgs(String name, String url, NavigationMode mode)
        {
            Name = name;
            Url = url;
            NavigationMode = mode;
        }

        internal String Name { get; private set; }
        internal String Url { get; private set; }
        internal NavigationMode NavigationMode { get; private set; }
    }
}