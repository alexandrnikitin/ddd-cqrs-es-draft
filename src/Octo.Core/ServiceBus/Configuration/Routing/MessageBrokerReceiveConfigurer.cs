using System;
using System.Linq;

using Octo.Core.ServiceBus.MessageBrokers;

namespace Octo.Core.ServiceBus.Configuration.Routing
{
    public class MessageBrokerReceiveConfigurer
    {
        protected readonly ServiceBusConfiguration ServiceBusConfiguration;
        protected Type Message;
        protected Type Publisher;

        public MessageBrokerReceiveConfigurer(Type message, ServiceBusConfiguration serviceBusConfiguration)
        {
            this.ServiceBusConfiguration = serviceBusConfiguration;
            this.Message = message;
        }

        public MessageBrokerReceiveAndConfigurer From<TReceiver>() where TReceiver : IReceiveMessages
        {
            var mbac = this.From(typeof(TReceiver));
            return mbac;
        }

        public MessageBrokerReceiveAndConfigurer From(Type receiveFrom)
        {
            var messageRoute = this.ServiceBusConfiguration.GetOrCreateMessageRouteFor(this.Message);
            var hasReceiveFrom = messageRoute.ReceiveFrom.Any(m => m.ReceiveFrom == receiveFrom);
            this.Publisher = receiveFrom;

            if (!hasReceiveFrom)
            {
                var mbr = new MessageBrokerRoute(receiveFrom);
                messageRoute.ReceiveFrom.Add(mbr);
            }

            var mbac = new MessageBrokerReceiveAndConfigurer(this.Message, this.ServiceBusConfiguration);
            return mbac;
        }
    }
}