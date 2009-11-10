namespace Meridian
{
    public interface IMvcHandler
    {
        void ProcessRequest(string url);
        void ProcessRequest(string url, ViewDataDictionary viewData);
    }
}
