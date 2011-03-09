using System;
using System.Collections.Generic;
using System.Linq;

namespace Meridian.Routing
{
    public class Route : RouteBase
    {
        private ParsedRoute _parsedRoute;

        private String _url = String.Empty;

        public String Url
        {
            get { return _url; }
            set
            {
                _url = value;
                _parsedRoute = RouteParser.Parse(_url);
            }
        }

        public RouteValueDictionary Defaults { get; set; }
      
        private RouteValueDictionary GetQueryStringValues(String queryString)
        {
            Requires.NotNullOrEmpty(queryString, "queryString");

            String[] values = queryString.Split(new []{'&'}, StringSplitOptions.RemoveEmptyEntries);
            RouteValueDictionary queryValues = new RouteValueDictionary();            
            foreach (var queryValue in values)
            {
                String key = queryValue.Substring(0,queryValue.IndexOf('='));
                String value = queryValue.Substring(queryValue.IndexOf('=')+1);
                queryValues.Add(key, value);
            }
            return queryValues;
        }

        public override RouteData GetRouteData(String url)
        {
            String virtualPath = url;
            if(virtualPath.StartsWith("/"))
            {
                virtualPath = virtualPath.Remove(0,1);
            }
            String queryString = null;
            if (virtualPath.Contains('?'))
            {
                queryString = virtualPath.Substring(virtualPath.IndexOf('?') + 1);
                virtualPath = virtualPath.Substring(0,virtualPath.IndexOf('?'));
            }
            RouteValueDictionary values = _parsedRoute.Match(virtualPath, Defaults);
            if (values == null)
            {
                return null;
            }
            RouteData data = new RouteData(this);
            
            foreach (KeyValuePair<String, Object> valuePair in values)
            {
                data.Values.Add(valuePair.Key, valuePair.Value);
            }
            if (queryString != null)
            {
                RouteValueDictionary queryStringValues = GetQueryStringValues(queryString);
                foreach (var queryPair in queryStringValues)
                {
                    data.Values.Add(queryPair.Key, queryPair.Value);
                }
            }
            return data;
        }
    }
}