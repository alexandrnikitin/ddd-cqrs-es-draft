using Octo.Core.ServiceBus.Chains.Receive;

namespace Octo.Core.ServiceBus.MessageBrokers.AzureServiceBus.Chains.Receive
{
    public interface IAzureReceiveMessageChain
    {
        void Receive(ReceiveMessageContext context);
    }
}