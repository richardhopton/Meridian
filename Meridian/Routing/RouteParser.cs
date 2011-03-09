using System;
using System.Collections.Generic;
using System.Globalization;

namespace Meridian.Routing
{
    internal sealed class RouteParser
    {
        public static IList<String> SplitUrlToPathStrings(String url)
        {
            Requires.NotNull(url, "url");

            List<String> parsedUrl = new List<String>();

            Int32 index;
            for (Int32 i = 0; i < url.Length; i = index + 1)
            {
                index = url.IndexOf('/', i);
                if (index == -1)
                {
                    String str = url.Substring(i);
                    if (!String.IsNullOrEmpty(str))
                    {
                        parsedUrl.Add(str);
                    }
                    return parsedUrl;
                }
                String item = url.Substring(i, index - i);
                if (!String.IsNullOrEmpty(item))
                {
                    parsedUrl.Add(item);
                }
                parsedUrl.Add("/");
            }
            return parsedUrl;
        }

        private static Int32 IndexOfFirstOpenParameter(String segment, Int32 startIndex)
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

        private static String GetLiteral(String segmentLiteral)
        {
            String str = segmentLiteral.Replace("{{", "").Replace("}}", "");
            if (!str.Contains("{") && !str.Contains("}"))
            {
                return segmentLiteral.Replace("{{", "{").Replace("}}", "}");
            }
            return null;
        }

        private static IList<PathSubSegment> ParseUrlSegment(String segment)
        {
            Requires.NotNull(segment, "segment");

            String routeParameter = "routeUrl";
            Int32 startIndex = 0;
            List<PathSubSegment> returnList = new List<PathSubSegment>();
            
            while (startIndex < segment.Length)
            {
                Int32 paramIndex = IndexOfFirstOpenParameter(segment, startIndex);
                if (paramIndex == -1)
                {
                    String literalSubSegment = GetLiteral(segment.Substring(startIndex));
                    if (literalSubSegment == null)
                    {                        
                        throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture, Strings.Route_MismatchedParameter, new Object[] { segment }), routeParameter);
                    }
                    if (literalSubSegment.Length > 0)
                    {
                        returnList.Add(new LiteralSubSegment(literalSubSegment));
                    }
                    break;
                }
                Int32 endIndex = segment.IndexOf('}', paramIndex + 1);
                if (endIndex == -1)
                {                    
                    throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture, Strings.Route_MismatchedParameter, new Object[] { segment }), routeParameter);
                }
                String literal = GetLiteral(segment.Substring(startIndex, paramIndex - startIndex));
                if (literal == null)
                {
                    throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture, Strings.Route_MismatchedParameter, new Object[] { segment }), routeParameter);
                }
                if (literal.Length > 0)
                {
                    returnList.Add(new LiteralSubSegment(literal));
                }
                String parameterName = segment.Substring(paramIndex + 1, (endIndex - paramIndex) - 1);
                returnList.Add(new ParameterSubSegment(parameterName));
                startIndex = endIndex + 1;
            }
            return returnList;
        }

        public static IList<PathSegment> SplitUrlToPathSegments(IList<String> urlParts)
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

        internal static Boolean IsSeparator(String value)
        {
            return String.Equals(value, "/", StringComparison.Ordinal);
        }

        public static ParsedRoute Parse(String routeUrl)
        {
            if (routeUrl == null)
            {
                routeUrl = String.Empty;
            }
            if ((routeUrl.StartsWith("~", StringComparison.Ordinal) || routeUrl.StartsWith("/", StringComparison.Ordinal)) || (routeUrl.IndexOf('?') != -1))
            {
                throw new ArgumentException("Invalid Route Url", "routeUrl");
            }
            IList<String> pathSegments = SplitUrlToPathStrings(routeUrl);
            return new ParsedRoute(SplitUrlToPathSegments(pathSegments));
        }
    }
}
