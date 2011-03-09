using System;

namespace Meridian
{
    public interface IControllerFactory
    {
        IController CreateController(String controllerName);
    }
}
