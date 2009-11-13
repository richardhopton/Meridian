namespace Meridian
{
    public interface IMvcHandler
    {
        event ProcessRequestEventHandler Processing;
        void ProcessRequest(string url);
        void ProcessRequest(string url, ViewDataDictionary viewData);
        void ProcessRequest(string url, ViewDataDictionary viewData, string verb);
    }
}
