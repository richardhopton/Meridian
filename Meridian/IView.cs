using System;

namespace Meridian
{
    public interface IView
    {
        void Render(ViewContext context);
    }
}
