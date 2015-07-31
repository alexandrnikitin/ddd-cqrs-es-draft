using System;
using System.Linq;

using Octo.ServiceBus.ServiceBus.MessageBrokers;

namespace Octo.ServiceBus.ServiceBus.Configuration.Routing
{
    public class MessageBrokerAndConfigurer : MessageBrokerConfigurer
    {
        public MessageBrokerAndConfigurer(Type message, ServiceBusConfiguration serviceBusConfiguration)
            :
                base(message, serviceBusConfiguration)
        {
        }

        public MessageBrokerAndConfigurer AndTo<TPublisher>()
            where TPublisher : IPublishMessages
        {
            var mbac = this.AndTo(typeof(TPublisher));
            return mbac;
        }

        public MessageBrokerAndConfigurer AndTo(Type publisher)
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