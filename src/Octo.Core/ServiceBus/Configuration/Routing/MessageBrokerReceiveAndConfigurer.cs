using System;
using System.Linq;

using Octo.Core.ServiceBus.MessageBrokers;

namespace Octo.Core.ServiceBus.Configuration.Routing
{
    public class MessageBrokerReceiveAndConfigurer : MessageBrokerReceiveConfigurer
    {
        public MessageBrokerReceiveAndConfigurer(Type message, ServiceBusConfiguration serviceBusConfiguration)
            :
                base(message, serviceBusConfiguration)
        {
        }

        public MessageBrokerReceiveAndConfigurer AndFrom<TReceiver>()
            where TReceiver : IReceiveMessages
        {
            var mbac = this.AndFrom(typeof(TReceiver));
            return mbac;
        }

        public MessageBrokerReceiveAndConfigurer AndFrom(Type receiveFrom)
        {
            var messageRoute = this.ServiceBusConfiguration.GetOrCreateMessageRouteFor(this.Message);

            var hasReceiveFrom = messageRoute.ReceiveFrom.Any(m => m.ReceiveFrom == receiveFrom);

            if (!hasReceiveFrom)
            {
                var mbr = new MessageBrokerRoute(receiveFrom);
                messageRoute.ReceiveFrom.Add(mbr);
            }

            var mbac = new MessageBrokerReceiveAndConfigurer(this.Message, this.ServiceBusConfiguration);
            return mbac;
        }

        public MessageBrokerReceiveAndWithConfigurer With<THandler>(IRouteCondition routeCondition)
            where THandler : IMessageHandler
        {
            var mbac = this.With(typeof(THandler), routeCondition);
            return mbac;
        }

        public MessageBrokerReceiveAndWithConfigurer With(Type handler, IRouteCondition routeCondition)
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