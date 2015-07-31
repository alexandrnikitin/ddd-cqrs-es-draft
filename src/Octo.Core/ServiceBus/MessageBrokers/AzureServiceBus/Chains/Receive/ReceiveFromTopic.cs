using Octo.Core.ServiceBus.Chains.Receive;
using Octo.Core.ServiceBus.Common;

namespace Octo.Core.ServiceBus.MessageBrokers.AzureServiceBus.Chains.Receive
{
    internal class ReceiveFromTopic : IReceiveMessageHandler
    {
        private readonly AzureServiceBusSubscriptionClientWrapper _subscriptionClientWrapper;

        public ReceiveFromTopic(AzureServiceBusSubscriptionClientWrapper subscriptionClientWrapper)
        {
            _subscriptionClientWrapper = subscriptionClientWrapper;
        }

        public void Handle(ReceiveMessageContext context)
        {
            var brokeredMessage = _subscriptionClientWrapper.Client.Receive();

            // todo 1341 should we continue the Receive chain if message is not found?
            if (brokeredMessage == null) return;

            var messageEnvelop = MessageEnvelop.FromBrokeredMessage(brokeredMessage);

            // todo 1341 Complete() should be done after successful processing
            brokeredMessage.Complete();
            context.MessageEnvelop = messageEnvelop;
        }
    }
}