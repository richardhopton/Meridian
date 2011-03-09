using System;
using System.Collections.Generic;
using System.Windows;

namespace Meridian.SL
{
    public static class FrameManager
    {
        private static readonly Dictionary<String, IFrame> _waitingFrames = new Dictionary<String, IFrame>();

        public static Boolean Remove(String url)
        {
            return _waitingFrames.Remove(url);
        }

        public static void Add(String url, IFrame frame)
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

        public static void Display(String url, UIElement element)
        {
            Requires.NotNull(url, "url");
            Requires.NotNull(element, "element");

            IFrame frame = null;
            if(_waitingFrames.TryGetValue(url, out frame))
            {
                frame.Display(url, element);
                Remove(url);
            }
        }
    }
}
