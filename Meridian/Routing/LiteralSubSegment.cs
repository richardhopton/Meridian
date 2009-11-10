namespace Meridian.Routing
{
    internal class LiteralSubSegment : PathSubSegment
    {
        public string Literal { get; set; }

        public LiteralSubSegment(string literal)
        {
            Literal = literal;
        }
    }
}