namespace Meridian.Routing
{
    public class RouteTable
    {
        private static RouteCollection _routes = new RouteCollection();
        
        public static RouteCollection Routes
        {
            get { return _routes; }
        }
    }
}
