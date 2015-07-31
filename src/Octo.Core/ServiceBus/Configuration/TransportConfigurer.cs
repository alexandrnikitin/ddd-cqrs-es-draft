using System;

using Autofac;

using Octo.Core.ServiceBus.MessageBrokers;

namespace Octo.Core.ServiceBus.Configuration
{
    public class TransportConfigurer : BaseConfigurer
    {
        public TransportConfigurer(ContainerBuilder containerBuilder, ServiceBusConfiguration serviceBusConfiguration)
            : base(containerBuilder, serviceBusConfiguration)
        {
        }

        public void AddPublisher<TPublisher>() where TPublisher : IPublishMessages
        {
            ServiceBusConfiguration.Publishers.Add(typeof(TPublisher));
        }

        public void AddPublisher(Type publisher)
        {
            // todo add checks
            ServiceBusConfiguration.Publishers.Add(publisher);
        }

        public void AddReceiver<TReceiver>() where TReceiver : IReceiveMessages
        {
            ServiceBusConfiguration.Receivers.Add(typeof(TReceiver));
        }

        public void AddReceiver(Type receiver)
        {
            ServiceBusConfiguration.Receivers.Add(receiver);
        }

        public void AddSender<TSender>() where TSender : ISendMessages
        {
            ServiceBusConfiguration.Senders.Add(typeof(TSender));
        }

        public void AddSender(Type sender)
        {
            ServiceBusConfiguration.Senders.Add(sender);
        }
    }
}