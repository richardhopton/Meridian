using System;
using Meridian.Routing;

namespace Meridian
{
    public interface IMvcHandler
    {
        event ProcessRequestEventHandler Processing;
        void ProcessRequest(String url);
        void ProcessRequest(String url, RequestParameters parameters);
        void ProcessRequest(String url, RequestParameters parameters, String verb);
    }
}
