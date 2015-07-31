using System.Collections.Generic;

using Octo.Core.Patterns.ChainOfHandlers;
using Octo.Core.Patterns.ChainOfHandlers.Interfaces;
using Octo.Core.ServiceBus.Common;

namespace Octo.Core.ServiceBus.Chains.Send
{
    internal class SendMessageChain : ChainOfHandlers<MessageContext, ISendMessageHandler>, ISendMessageChain
    {
        private readonly IContextManager _contextManager;

        public SendMessageChain(
            IHandlingStrategy<MessageContext, ISendMessageHandler> handlingStrategy, 
            IEnumerable<ISendMessageHandler> handlers, 
            IContextManager contextManager)
            : base(handlingStrategy, handlers)
        {
            _contextManager = contextManager;
        }

        public void Send(IMessage message)
        {
            var messageEnvelop = new MessageEnvelop(message);
            var context = new MessageContext(_contextManager.Current, messageEnvelop);
            _contextManager.Push(context);
            Process(context);
            _contextManager.Pop();
        }
    }
}