using System.Collections.Generic;

using Octo.Core.ServiceBus.Chains.Receive;
using Octo.Core.ServiceBus.Subscriptions;

namespace Octo.Core.ServiceBus.MessageBrokers.Memory.Chains.Receive.Handlers
{
    internal class FindMessageHandlers : IReceiveMessageHandler
    {
        private readonly IMessageBrokerManager _messageBrokerManager;

        public FindMessageHandlers(IMessageBrokerManager messageBrokerManager)
        {
            _messageBrokerManager = messageBrokerManager;
        }

        public void Handle(ReceiveMessageContext context)
        {
            // todo get rid of reflection, cache it?
            IMessageEnvelop messageEnvelop;
            if (!context.TryGetMessageEnvelop(out messageEnvelop)) return;

            var handlers = _messageBrokerManager.GetHandlers(messageEnvelop.MessageType, context.MessageBrokerType);

            context.MessageHandlerContainers = new List<IMessageHandlerContainer>();
            context.MessageHandlerContainers.AddRange(handlers);

            // todo don't continue the chain if we didn't find the handler
        }
    }
}