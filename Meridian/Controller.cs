namespace Meridian
{
    public class Controller : ControllerBase
    {
        public override void Execute()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("Action");

            ActionInvoker.InvokeAction(ControllerContext, actionName);
        }
    }
}
