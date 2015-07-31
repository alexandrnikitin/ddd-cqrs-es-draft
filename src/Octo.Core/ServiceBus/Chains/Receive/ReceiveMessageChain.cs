using System.Collections.Generic;

namespace Octo.Core.ServiceBus.Chains.Receive
{
    internal class ReceiveMessageChain
        : ChainOfHandlers<ReceiveMessageContext, IReceiveMessageHandler>, IReceiveMessageChain
    {
        private readonly IContextManager _contextManager;

        public ReceiveMessageChain(
            IHandlingStrategy<ReceiveMessageContext, IReceiveMessageHandler> handlingStrategy, 
            IEnumerable<IReceiveMessageHandler> handlers, 
            IContextManager contextManager)
            : base(handlingStrategy, handlers)
        {
            _contextManager = contextManager;
        }

        public void Receive()
        {
            var currentContext = _contextManager.Current;
            var context = new ReceiveMessageContext(currentContext);
            Process(context);
        }
    }
}