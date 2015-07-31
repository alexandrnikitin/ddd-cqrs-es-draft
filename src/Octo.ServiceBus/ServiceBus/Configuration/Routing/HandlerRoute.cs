using System;

namespace Octo.ServiceBus.ServiceBus.Configuration.Routing
{
    public class HandlerRoute
    {
        public HandlerRoute(Type handler, IRouteCondition condition)
        {
            this.Handler = handler;
            this.Condition = condition;
        }

        public IRouteCondition Condition { get; private set; }
        public Type Handler { get; private set; }
    }
}