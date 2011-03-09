using System;

namespace Meridian
{
    public interface IViewEngine
    {
        IView GetView(ControllerContext controllerContext, String viewName);
    }
}
