using System.Collections.Generic;

using Octo.Core.Patterns.ChainOfHandlers;
using Octo.Core.Patterns.ChainOfHandlers.Interfaces;
using Octo.Core.ServiceBus.Chains.Receive;

namespace Octo.Core.ServiceBus.MessageBrokers.Memory.Chains.Receive
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