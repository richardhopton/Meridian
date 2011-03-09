using System;

namespace Meridian
{
    public interface IAsyncController
    {        
        event EventHandler<ActionResultEventArgs> ActionCompleted;
    }
}
