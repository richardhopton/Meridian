using Meridian.Routing;
namespace Meridian
{
    public interface IMvcHandler
    {
        event ProcessRequestEventHandler Processing;
        void ProcessRequest(string url);
        void ProcessRequest(string url, RequestParameters parameters);
        void ProcessRequest(string url, RequestParameters parameters, string verb);
    }
}
