namespace Octo.Core.ServiceBus.MessageBrokers.AzureServiceBus
{
    public class AzureServiceBusSubscriptionClientWrapper
    {
        public AzureServiceBusSubscriptionClientWrapper(SubscriptionClient client)
        {
            Client = client;
        }

        public SubscriptionClient Client { get; private set; }
    }
}