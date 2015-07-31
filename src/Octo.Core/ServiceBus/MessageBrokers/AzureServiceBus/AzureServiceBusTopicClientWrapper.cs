namespace Octo.Core.ServiceBus.MessageBrokers.AzureServiceBus
{
    public class AzureServiceBusTopicClientWrapper
    {
        public AzureServiceBusTopicClientWrapper(TopicClient client)
        {
            Client = client;
        }

        public TopicClient Client { get; private set; }
    }
}