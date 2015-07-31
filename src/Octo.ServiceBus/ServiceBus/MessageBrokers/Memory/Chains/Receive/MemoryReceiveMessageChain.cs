using System.Collections.Generic;

using Octo.ServiceBus.ServiceBus.Chains.Receive;

namespace Octo.ServiceBus.ServiceBus.MessageBrokers.Memory.Chains.Receive
{
    internal class MemoryReceiveMessageChain
        : ChainOfHandlers<ReceiveMessageContext, IReceiveMessageHandler>, IMemoryReceiveMessageChain
    {
        public MemoryReceiveMessageChain(
            IHandlingStrategy<ReceiveMessageContext, IReceiveMessageHandler> handlingStrategy, 
            IEnumerable<IReceiveMessageHandler> handlers)
            : base(handlingStrategy, handlers)
        {
        }

        public void Receive(ReceiveMessageContext context)
        {
            Process(context);
        }
    }
}