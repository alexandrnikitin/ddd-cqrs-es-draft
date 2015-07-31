using System;

using Octo.ServiceBus.ServiceBus.Configuration.Routing;
using Octo.ServiceBus.ServiceBus.MessageBrokers;

namespace Octo.ServiceBus.ServiceBus.Configuration
{
    public class RouteConfigurer : BaseConfigurer
    {
        public RouteConfigurer(ContainerBuilder containerBuilder, ServiceBusConfiguration serviceBusConfiguration)
            : base(containerBuilder, serviceBusConfiguration)
        {
        }

        public MessageBrokerConfigurer Publish<TMessage>()
            where TMessage : IMessage
        {
            var mbConfigurer = Publish(typeof(TMessage));
            return mbConfigurer;
        }

        public MessageBrokerConfigurer Publish(Type message)
        {
            // todo add checks
            var mbConfigurer = new MessageBrokerConfigurer(message, ServiceBusConfiguration);
            ServiceBusConfiguration.GetOrCreateMessageRouteFor(message);
            return mbConfigurer;
        }

        public MessageBrokerReceiveConfigurer Receive<TMessage>()
            where TMessage : IMessage
        {
            var mbConfigurer = Receive(typeof(TMessage));
            return mbConfigurer;
        }

        public MessageBrokerReceiveConfigurer Receive(Type message)
        {
            // todo add checks
            var mbConfigurer = new MessageBrokerReceiveConfigurer(message, ServiceBusConfiguration);
            ServiceBusConfiguration.GetOrCreateMessageRouteFor(message);
            return mbConfigurer;
        }

        public void Send<TMessage, TSender>()
            where TMessage : IMessage
            where TSender : ISendMessages
        {
            throw new NotImplementedException();
        }
    }
}