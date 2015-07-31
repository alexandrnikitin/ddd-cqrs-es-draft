using Octo.ServiceBus.ServiceBus.Common;
using Octo.ServiceBus.ServiceBus.MessageBrokers.AzureServiceBus.Chains.Publish;

namespace Octo.ServiceBus.ServiceBus.MessageBrokers.AzureServiceBus
{
    public class AzureServiceBusPublishMessages : IPublishMessages
    {
        private readonly IAzurePublishMessageChain _azurePublishMessageChain;

        public AzureServiceBusPublishMessages(IAzurePublishMessageChain azurePublishMessageChain)
        {
            _azurePublishMessageChain = azurePublishMessageChain;
        }

        public void Publish(MessageContext messageContext)
        {
            _azurePublishMessageChain.Publish(messageContext);
        }
    }
}