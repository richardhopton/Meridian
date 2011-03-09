using System;

namespace Meridian
{
    public interface IController
    {
        void Execute(RequestContext context);
    }
}
