using System;
using System.Linq;

using Octo.ServiceBus.ServiceBus.MessageBrokers;

namespace Octo.ServiceBus.ServiceBus.Configuration.Routing
{
    public class MessageBrokerConfigurer
    {
        protected readonly ServiceBusConfiguration ServiceBusConfiguration;
        protected Type Message;

        public MessageBrokerConfigurer(Type message, ServiceBusConfiguration serviceBusConfiguration)
        {
            this.ServiceBusConfiguration = serviceBusConfiguration;
            this.Message = message;
        }

        public MessageBrokerAndConfigurer To<TPublisher>()
            where TPublisher : IPublishMessages
        {
            var mbac = this.To(typeof(TPublisher));
            return mbac;
        }

        public MessageBrokerAndConfigurer To(Type publisher)
        {
            var messageRoute = this.ServiceBusConfiguration.GetOrCreateMessageRouteFor(this.Message);

            var hasPublisher = messageRoute.PublishTo.Any(m => m == publisher);

            if (!hasPublisher)
            {
                messageRoute.PublishTo.Add(publisher);
            }

            var mbac = new MessageBrokerAndConfigurer(this.Message, this.ServiceBusConfiguration);
            return mbac;
        }
    }
}