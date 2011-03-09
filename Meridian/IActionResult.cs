using System;

namespace Meridian
{
    public interface IActionResult
    {
        void Execute(ActionContext context);
    }
}
