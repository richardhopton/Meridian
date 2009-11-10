using System;
using System.Collections.Generic;
using System.Globalization;

namespace Meridian.Routing
{
    internal sealed class RouteParser
    {
        public static IList<string> SplitUrlToPathStrings(string url)
        {
            Requires.NotNull(url, "url");

            List<string> parsedUrl = new List<string>();

            int index;
            for (int i = 0; i < url.Length; i = index + 1)
            {
                index = url.IndexOf('/', i);
                if (index == -1)
                {
                    string str = url.Substring(i);
                    if (!string.IsNullOrEmpty(str))
                    {
                        parsedUrl.Add(str);
                    }
                    return parsedUrl;
                }
                string item = url.Substring(i, index - i);
                if (!string.IsNullOrEmpty(item))
                {
                    parsedUrl.Add(item);
                }
                parsedUrl.Add("/");
            }
            return parsedUrl;
        }

        private static int IndexOfFirstOpenParameter(string segment, int startIndex)
        {
            while (true)
            {
                startIndex = segment.IndexOf('{', startIndex);
                if (startIndex == -1)
                {
                    return -1;
                }
                if (((startIndex + 1) == segment.Length) || (((startIndex + 1) < segment.Length) && (segment[startIndex + 1] != '{')))
                {
                    return startIndex;
                }
                startIndex += 2;
            }
        }

        private static string GetLiteral(string segmentLiteral)
        {
            string str = segmentLiteral.Replace("{{", "").Replace("}}", "");
            if (!str.Contains("{") && !str.Contains("}"))
            {
                return segmentLiteral.Replace("{{", "{").Replace("}}", "}");
            }
            return null;
        }

        private static IList<PathSubSegment> ParseUrlSegment(string segment)
        {
            Requires.NotNull(segment, "segment");

            string routeParameter = "routeUrl";
            int startIndex = 0;
            List<PathSubSegment> returnList = new List<PathSubSegment>();
            
            while (startIndex < segment.Length)
            {
                int paramIndex = IndexOfFirstOpenParameter(segment, startIndex);
                if (paramIndex == -1)
                {
                    string literalSubSegment = GetLiteral(segment.Substring(startIndex));
                    if (literalSubSegment == null)
                    {                        
                        throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Strings.Route_MismatchedParameter, new object[] { segment }), routeParameter);
                    }
                    if (literalSubSegment.Length > 0)
                    {
                        returnList.Add(new LiteralSubSegment(literalSubSegment));
                    }
                    break;
                }
                int endIndex = segment.IndexOf('}', paramIndex + 1);
                if (endIndex == -1)
                {                    
                    throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Strings.Route_MismatchedParameter, new object[] { segment }), routeParameter);
                }
                string literal = GetLiteral(segment.Substring(startIndex, paramIndex - startIndex));
                if (literal == null)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Strings.Route_MismatchedParameter, new object[] { segment }), routeParameter);
                }
                if (literal.Length > 0)
                {
                    returnList.Add(new LiteralSubSegment(literal));
                }
                string parameterName = segment.Substring(paramIndex + 1, (endIndex - paramIndex) - 1);
                returnList.Add(new ParameterSubSegment(parameterName));
                startIndex = endIndex + 1;
            }
            return returnList;
        }

        public static IList<PathSegment> SplitUrlToPathSegments(IList<string> urlParts)
        {
            Requires.NotNull(urlParts, "urlParts");

            List<PathSegment> returnList = new List<PathSegment>();

            foreach (var part in urlParts)
            {
                if (IsSeparator(part))
                {
                    returnList.Add(new SeparatorPathSegment());
                }
                else
                {
                    IList<PathSubSegment> subsegments = ParseUrlSegment(part);
                    returnList.Add(new ContentPathSegment(subsegments));
                }
            }
            return returnList;
        }

        internal static bool IsSeparator(string value)
        {
            return string.Equals(value, "/", StringComparison.Ordinal);
        }

        public static ParsedRoute Parse(string routeUrl)
        {
            if (routeUrl == null)
            {
                routeUrl = string.Empty;
            }
            if ((routeUrl.StartsWith("~", StringComparison.Ordinal) || routeUrl.StartsWith("/", StringComparison.Ordinal)) || (routeUrl.IndexOf('?') != -1))
            {
                throw new ArgumentException("Invalid Route Url", "routeUrl");
            }
            IList<string> pathSegments = SplitUrlToPathStrings(routeUrl);
            return new ParsedRoute(SplitUrlToPathSegments(pathSegments));
        }
    }
}
