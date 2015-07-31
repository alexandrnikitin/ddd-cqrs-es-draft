using Octo.ServiceBus.ServiceBus.Chains.Publish;
using Octo.ServiceBus.ServiceBus.Chains.Receive;
using Octo.ServiceBus.ServiceBus.Common;
using Octo.ServiceBus.ServiceBus.Configuration;
using Octo.ServiceBus.ServiceBus.MessageBrokers.AzureServiceBus.Chains.Publish;
using Octo.ServiceBus.ServiceBus.MessageBrokers.AzureServiceBus.Chains.Receive;
using Octo.ServiceBus.ServiceBus.MessageBrokers.Memory.Chains.Receive.Handlers;

namespace Octo.ServiceBus.ServiceBus.MessageBrokers.AzureServiceBus
{
    public static class AzureServiceBusTransportConfigurerExtensions
    {
        public static void UseAzureServiceBus(
            this TransportConfigurer configurer, 
            string connectionString, 
            string topicName, 
            string subscriptionName)
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            // todo 1341 what if we don't have permissions, check, try catch?
            if (!namespaceManager.TopicExists(topicName))
            {
                namespaceManager.CreateTopic(topicName);
            }
            if (!namespaceManager.SubscriptionExists(topicName, subscriptionName))
            {
                namespaceManager.CreateSubscription(topicName, subscriptionName);
            }

            configurer.AddPublisher<AzureServiceBusPublishMessages>();
            configurer.ContainerBuilder.RegisterType<AzureServiceBusPublishMessages>();

            var topicClient = TopicClient.CreateFromConnectionString(connectionString, topicName);
            var topicClientWrapper = new AzureServiceBusTopicClientWrapper(topicClient);
            configurer.ContainerBuilder.RegisterInstance(topicClientWrapper);

            configurer.AddReceiver<AzureServiceBusReceiveMessages>();
            configurer.ContainerBuilder.RegisterType<AzureServiceBusReceiveMessages>();

            var subscriptionClient = SubscriptionClient.CreateFromConnectionString(
                connectionString, 
                topicName, 
                subscriptionName);
            var subscriptionClientWrapper = new AzureServiceBusSubscriptionClientWrapper(subscriptionClient);
            configurer.ContainerBuilder.RegisterInstance(subscriptionClientWrapper);

            configurer.ContainerBuilder.RegisterType<PublishToTopic>();
            configurer.ContainerBuilder.Register(
                c =>
                new AzurePublishMessageChain(
                    c.Resolve<AllHandlingStrategy<MessageContext, IPublishMessageHandler>>(), 
                    new[] { c.Resolve<PublishToTopic>() }
                    ))
                .As<IAzurePublishMessageChain>();

            configurer.ContainerBuilder.RegisterType<ReceiveFromTopic>();

            // todo 1341 FindMessageHandlers and HandleMessage are in MemoryServiceBus namespace
            configurer.ContainerBuilder.RegisterType<FindMessageHandlers>();
            configurer.ContainerBuilder.RegisterType<HandleMessage>();
            configurer.ContainerBuilder.Register(
                c =>
                new AzureReceiveMessageChain(
                    c.Resolve<AllHandlingStrategy<ReceiveMessageContext, IReceiveMessageHandler>>(), 
                    new IReceiveMessageHandler[]
                        { c.Resolve<ReceiveFromTopic>(), c.Resolve<FindMessageHandlers>(), c.Resolve<HandleMessage>() }
                    ))
                .As<IAzureReceiveMessageChain>();
        }
    }
}