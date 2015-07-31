using Octo.ServiceBus.ServiceBus.Chains.Receive;

namespace Octo.ServiceBus.ServiceBus.MessageBrokers.Memory.Chains.Receive
{
    public interface IMemoryReceiveMessageChain
    {
        void Receive(ReceiveMessageContext context);
    }
}