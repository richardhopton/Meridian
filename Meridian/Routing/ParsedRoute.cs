using System;
using System.Collections.Generic;

namespace Meridian.Routing
{
    internal class ParsedRoute
    {
        public IList<PathSegment> PathSegments { get; set; }

        public ParsedRoute(IList<PathSegment> pathSegments)
        {
            PathSegments = pathSegments;
        }

        public RouteValueDictionary Match(string virtualPath, RouteValueDictionary defaultValues)
        {
            IList<string> source = RouteParser.SplitUrlToPathStrings(virtualPath);

            var matchedValues = new RouteValueDictionary();

            bool outOfRange = false;
            for (int i = 0; i < PathSegments.Count; i++)
            {
                PathSegment segment = PathSegments[i];
                if (source.Count <= i)
                {
                    outOfRange = true;
                }
                string subString = outOfRange ? null : source[i];
                if (segment is SeparatorPathSegment)
                {
                    if (!outOfRange && !string.Equals(subString, "/", StringComparison.Ordinal))
                    {
                        return null;
                    }
                }
                else
                {
                    ContentPathSegment contentPathSegment = segment as ContentPathSegment;
                    if (contentPathSegment != null)
                    {
                        if (!MatchContentPathSegment(contentPathSegment, subString, defaultValues, matchedValues))
                        {
                            return null;
                        }
                    }
                }
            }
            if (PathSegments.Count < source.Count)
            {
                for (int j = PathSegments.Count; j < source.Count; j++)
                {
                    if (!RouteParser.IsSeparator(source[j]))
                    {
                        return null;
                    }
                }
            }
            if (defaultValues != null)
            {
                foreach (KeyValuePair<string, object> pair in defaultValues)
                {
                    if (!matchedValues.ContainsKey(pair.Key))
                    {
                        matchedValues.Add(pair.Key, pair.Value);
                    }
                }
            }
            return matchedValues;
        }

        private bool MatchContentPathSegment(ContentPathSegment routeSegment, string requestPathSegment, IDictionary<string, object> defaultValues, IDictionary<string, object> matchedValues)
        {
            if (string.IsNullOrEmpty(requestPathSegment))
            {
                if (routeSegment.SubSegments.Count <= 1)
                {
                    object segmentValue;
                    ParameterSubSegment subsegment = routeSegment.SubSegments[0] as ParameterSubSegment;
                    if (subsegment == null)
                    {
                        return false;
                    }
                    if (defaultValues.TryGetValue(subsegment.ParameterName, out segmentValue))
                    {
                        matchedValues.Add(subsegment.ParameterName, segmentValue);
                        return true;
                    }
                }
                return false;
            }
            int length = requestPathSegment.Length;
            int subsegmentIndex = routeSegment.SubSegments.Count - 1;
            ParameterSubSegment parameterSubSegment;
            LiteralSubSegment literalSubSegment;
            while (subsegmentIndex >= 0)
            {
                int segmentLength = length;

                parameterSubSegment = routeSegment.SubSegments[subsegmentIndex] as ParameterSubSegment;
                literalSubSegment = routeSegment.SubSegments[subsegmentIndex] as LiteralSubSegment;
                if (literalSubSegment != null)
                {
                    int endIndex = requestPathSegment.LastIndexOf(literalSubSegment.Literal, length - 1, StringComparison.OrdinalIgnoreCase);
                    if (endIndex == -1)
                    {
                        return false;
                    }
                    if ((subsegmentIndex == (routeSegment.SubSegments.Count - 1)) &&
                        ((endIndex + literalSubSegment.Literal.Length) != requestPathSegment.Length))
                    {
                        return false;
                    }
                    segmentLength = endIndex;
                }
                if ((parameterSubSegment != null) && ((literalSubSegment != null) || (subsegmentIndex == 0)))
                {
                    int startIndex;
                    int endIndex;
                    if (literalSubSegment == null)
                    {
                        startIndex = subsegmentIndex == 0 ? 0 : segmentLength;
                        endIndex = length;
                    }
                    else if (subsegmentIndex == 0)
                    {
                        startIndex = 0;
                        endIndex = length;
                    }
                    else
                    {
                        startIndex = segmentLength + literalSubSegment.Literal.Length;
                        endIndex = length - startIndex;
                    }
                    string str = requestPathSegment.Substring(startIndex, endIndex);
                    if (string.IsNullOrEmpty(str))
                    {
                        return false;
                    }
                    matchedValues.Add(parameterSubSegment.ParameterName, str);
                }
                length = segmentLength;
                subsegmentIndex--;
            }
            if (length != 0)
            {
                return (routeSegment.SubSegments[0] is ParameterSubSegment);
            }
            return true;
        }

    }
}
