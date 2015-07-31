using Octo.ServiceBus.ServiceBus.Chains.Receive;
using Octo.ServiceBus.ServiceBus.Configuration;
using Octo.ServiceBus.ServiceBus.MessageBrokers.Memory.Chains.Receive;
using Octo.ServiceBus.ServiceBus.MessageBrokers.Memory.Chains.Receive.Handlers;

namespace Octo.ServiceBus.ServiceBus.MessageBrokers.Memory
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