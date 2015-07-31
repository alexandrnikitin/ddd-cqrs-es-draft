using Octo.ServiceBus.ServiceBus.Chains.Receive;

namespace Octo.ServiceBus.ServiceBus.MessageBrokers.AzureServiceBus.Chains.Receive
{
    public interface IAzureReceiveMessageChain
    {
        void Receive(ReceiveMessageContext context);
    }
}