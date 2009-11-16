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
using System.Collections.Generic;

namespace Meridian.SL
{
    public static class FrameManager
    {
        private static readonly Dictionary<string, IFrame> _waitingFrames = new Dictionary<string, IFrame>();

        public static bool Remove(string url)
        {
            return _waitingFrames.Remove(url);
        }

        public static void Add(string url, IFrame frame)
        {
            if (_waitingFrames.ContainsKey(url))
            {
                _waitingFrames[url] = frame;
            }
            else
            {
                _waitingFrames.Add(url, frame);
            }
        }

        public static void Display(string url, UIElement element)
        {
            Requires.NotNull(url, "url");
            Requires.NotNull(element, "element");

            IFrame frame = null;
            if(_waitingFrames.TryGetValue(url, out frame))
            {
                frame.Display(element);
                Remove(url);
            }
        }
    }
}
