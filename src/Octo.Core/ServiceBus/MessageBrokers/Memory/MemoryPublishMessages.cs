using Octo.Core.ServiceBus.Common;

namespace Octo.Core.ServiceBus.MessageBrokers.Memory
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