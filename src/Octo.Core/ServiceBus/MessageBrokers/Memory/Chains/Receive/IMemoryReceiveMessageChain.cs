using Octo.Core.ServiceBus.Chains.Receive;

namespace Octo.Core.ServiceBus.MessageBrokers.Memory.Chains.Receive
{
    public interface IMemoryReceiveMessageChain
    {
        void Receive(ReceiveMessageContext context);
    }
}