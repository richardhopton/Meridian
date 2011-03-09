using System;

namespace Meridian
{
    public class ProcessRequestEventArgs : EventArgs
    {
        public String Uri { get; set; }
        public Boolean Cancel { get; set; }
        public String Verb { get; set; }

        public ProcessRequestEventArgs(String uri, Boolean cancel, String verb)
        {
            Uri = uri;
            Cancel = cancel;
            Verb = verb;
        }
    }
}
