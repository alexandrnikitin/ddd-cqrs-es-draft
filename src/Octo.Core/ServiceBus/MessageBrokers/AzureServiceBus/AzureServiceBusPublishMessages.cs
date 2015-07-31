using Octo.Core.ServiceBus.Common;
using Octo.Core.ServiceBus.MessageBrokers.AzureServiceBus.Chains.Publish;

namespace Octo.Core.ServiceBus.MessageBrokers.AzureServiceBus
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