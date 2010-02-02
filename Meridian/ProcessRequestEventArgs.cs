using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Meridian
{
    public class ProcessRequestEventArgs : EventArgs
    {
        public string Uri { get; set; }
        public bool Cancel { get; set; }
        public string Verb { get; set; }

        public ProcessRequestEventArgs(string uri, bool cancel, string verb)
        {
            Uri = uri;
            Cancel = cancel;
            Verb = verb;
        }
    }
}
