using Autofac;

using Octo.Core.Patterns.ChainOfHandlers.HandlingStrategies;
using Octo.Core.ServiceBus.Chains.Receive;
using Octo.Core.ServiceBus.Configuration;
using Octo.Core.ServiceBus.MessageBrokers.Memory.Chains.Receive;
using Octo.Core.ServiceBus.MessageBrokers.Memory.Chains.Receive.Handlers;

namespace Octo.Core.ServiceBus.MessageBrokers.Memory
{
    public static class MemoryServiceBusConfigurerExtensions
    {
        public static void UseMemoryServiceBus(this TransportConfigurer configurer)
        {
            configurer.AddPublisher<MemoryPublishMessages>();
            configurer.ContainerBuilder.RegisterType<MemoryPublishMessages>()
                
                // todo avoid the line below
                .WithParameter(
                    (info, context) => info.ParameterType == typeof(IReceiveMessages), 
                    (info, context) => context.Resolve<MemoryReceiveMessages>());

            configurer.AddReceiver<MemoryReceiveMessages>();
            configurer.ContainerBuilder.RegisterType<MemoryReceiveMessages>();

            configurer.AddSender<MemorySendMessages>();
            configurer.ContainerBuilder.RegisterType<MemorySendMessages>();

            

            configurer.ContainerBuilder.RegisterType<FindMessageHandlers>();
            configurer.ContainerBuilder.RegisterType<HandleMessage>();
            configurer.ContainerBuilder.Register(
                c =>
                new MemoryReceiveMessageChain(
                    c.Resolve<AllHandlingStrategy<ReceiveMessageContext, IReceiveMessageHandler>>(), 
                    new IReceiveMessageHandler[] { c.Resolve<FindMessageHandlers>(), c.Resolve<HandleMessage>() }))
                .As<IMemoryReceiveMessageChain>();

            
        }
    }
}