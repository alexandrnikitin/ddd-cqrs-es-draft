using Octo.ServiceBus.ServiceBus.Common;

namespace Octo.ServiceBus.ServiceBus.MessageBrokers.Memory
{
    public class MemoryPublishMessages : IPublishMessages
    {
        private readonly IReceiveMessages _receiveMessages;

        public MemoryPublishMessages(IReceiveMessages receiveMessages)
        {
            _receiveMessages = receiveMessages;
        }

        public void Publish(MessageContext context)
        {
            _receiveMessages.Receive();
        }
    }
}