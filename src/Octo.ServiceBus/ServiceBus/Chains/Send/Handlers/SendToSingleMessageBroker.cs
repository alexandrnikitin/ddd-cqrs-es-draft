using System.Linq;

using Octo.ServiceBus.ServiceBus.Common;
using Octo.ServiceBus.ServiceBus.Subscriptions;

namespace Octo.ServiceBus.ServiceBus.Chains.Send.Handlers
{
    internal class SendToSingleMessageBroker : ISendMessageHandler
    {
        private readonly IMessageBrokerManager _messageBrokerManager;

        public SendToSingleMessageBroker(IMessageBrokerManager messageBrokerManager)
        {
            _messageBrokerManager = messageBrokerManager;
        }

        public void Handle(MessageContext context)
        {
            var senders = _messageBrokerManager.GetSendersFor(context.MessageEnvelop.MessageType);

            // Should be just one receiver because we send a message as a command
            var sender = senders.Single();
            sender.Send(context.MessageEnvelop.Message);
        }
    }
}