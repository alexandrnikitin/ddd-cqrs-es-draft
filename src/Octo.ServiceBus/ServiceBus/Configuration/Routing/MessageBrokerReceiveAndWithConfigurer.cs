using System;

namespace Octo.ServiceBus.ServiceBus.Configuration.Routing
{
    public class MessageBrokerReceiveAndWithConfigurer : MessageBrokerReceiveAndConfigurer
    {
        public MessageBrokerReceiveAndWithConfigurer(Type message, ServiceBusConfiguration serviceBusConfiguration)
            : base(message, serviceBusConfiguration)
        {
        }

        public MessageBrokerReceiveAndWithConfigurer AndWith<THandler>(IRouteCondition routeCondition)
            where THandler : IMessageHandler
        {
            var mbac = this.AndWith(typeof(THandler), routeCondition);
            return mbac;
        }

        public MessageBrokerReceiveAndWithConfigurer AndWith(Type handler, IRouteCondition routeCondition)
        {
            var messageRoute = this.ServiceBusConfiguration.GetOrCreateMessageRouteFor(this.Message);

            foreach (var receiveFrom in messageRoute.ReceiveFrom)
            {
                receiveFrom.Handlers.Add(new HandlerRoute(handler, routeCondition));
            }

            var mbac = new MessageBrokerReceiveAndWithConfigurer(this.Message, this.ServiceBusConfiguration);
            return mbac;
        }
    }
}