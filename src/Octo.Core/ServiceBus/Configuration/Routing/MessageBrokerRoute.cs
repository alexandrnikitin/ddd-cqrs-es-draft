using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Octo.Core.ServiceBus.Configuration.Routing
{
    public class MessageBrokerRoute
    {
        public MessageBrokerRoute(Type receiveFrom)
        {
            this.ReceiveFrom = receiveFrom;
            this.Handlers = new Collection<HandlerRoute>();
        }

        public ICollection<HandlerRoute> Handlers { get; private set; }
        public Type ReceiveFrom { get; private set; }

        public IEnumerable<HandlerRoute> AcceptableHandlerRoutes(IRouteConditionContext context)
        {
            var route =
                this.Handlers.Where(
                    m => m.Condition == null || m.Condition.IsHandlerAcceptable(context));

            return route;
        }
    }
}