using System;

namespace Meridian.Routing
{
    internal class LiteralSubSegment : PathSubSegment
    {
        public String Literal { get; set; }

        public LiteralSubSegment(String literal)
        {
            Literal = literal;
        }
    }
}