using System.Collections.Generic;

namespace Meridian.Routing
{
    internal class ContentPathSegment : PathSegment
    {
        public IList<PathSubSegment> SubSegments { get; set; }

        public ContentPathSegment(IList<PathSubSegment> subsegments)
        {
            SubSegments = subsegments;
        }
    }
}