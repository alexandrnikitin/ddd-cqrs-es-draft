using System;

using Octo.ServiceBus.ServiceBus.Chains.Publish;
using Octo.ServiceBus.ServiceBus.Chains.Publish.Handlers;
using Octo.ServiceBus.ServiceBus.Chains.Receive;
using Octo.ServiceBus.ServiceBus.Chains.Receive.Handlers;
using Octo.ServiceBus.ServiceBus.Chains.Send;
using Octo.ServiceBus.ServiceBus.Chains.Send.Handlers;
using Octo.ServiceBus.ServiceBus.Common;
using Octo.ServiceBus.ServiceBus.Subscriptions;

namespace Octo.ServiceBus.ServiceBus.Configuration
{
    public class ServiceBusConfigurer : BaseConfigurer
    {
        public ServiceBusConfigurer(
            ContainerBuilder containerBuilder, 
            ServiceBusConfiguration serviceBusConfiguration)
            : base(containerBuilder, serviceBusConfiguration)
        {
        }

        public void Configure()
        {
            ContainerBuilder.RegisterType<MessageBrokerManager>().As<IMessageBrokerManager>();
            ContainerBuilder.RegisterType<ContextManager>().As<IContextManager>().InstancePerLifetimeScope();

            RegisterChains();

            ContainerBuilder.RegisterInstance(ServiceBusConfiguration);
            ContainerBuilder.RegisterType<Common.ServiceBus>().As<IServiceBus>();
        }

        public ServiceBusConfigurer Route(Action<RouteConfigurer> configurer)
        {
            configurer(new RouteConfigurer(ContainerBuilder, ServiceBusConfiguration));
            return this;
        }

        public ServiceBusConfigurer Transport(Action<TransportConfigurer> configurer)
        {
            configurer(new TransportConfigurer(ContainerBuilder, ServiceBusConfiguration));
            return this;
        }

        private void RegisterChains()
        {
            

            ContainerBuilder.RegisterType<ReceiveFromMessageBrokers>();
            ContainerBuilder.Register(
                c =>
                new ReceiveMessageChain(
                    c.Resolve<AllHandlingStrategy<ReceiveMessageContext, IReceiveMessageHandler>>(), 
                    new IReceiveMessageHandler[] { c.Resolve<ReceiveFromMessageBrokers>() }, 
                    c.Resolve<IContextManager>()))
                .As<IReceiveMessageChain>();

            

            ContainerBuilder.RegisterType<PublishToMulticastMessageBroker>();
            ContainerBuilder.Register(
                c =>
                new PublishMessageChain(
                    c.Resolve<AllHandlingStrategy<MessageContext, IPublishMessageHandler>>(), 
                    new[] { c.Resolve<PublishToMulticastMessageBroker>() }, 
                    c.Resolve<IContextManager>()))
                .As<IPublishMessageChain>();

            ContainerBuilder.RegisterType<SendToSingleMessageBroker>();
            ContainerBuilder.Register(
                c =>
                new SendMessageChain(
                    c.Resolve<AllHandlingStrategy<MessageContext, ISendMessageHandler>>(), 
                    new[] { c.Resolve<SendToSingleMessageBroker>() }, 
                    c.Resolve<IContextManager>()))
                .As<ISendMessageChain>();
        }
    }
}