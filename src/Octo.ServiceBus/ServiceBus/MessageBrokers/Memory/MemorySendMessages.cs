namespace Octo.ServiceBus.ServiceBus.MessageBrokers.Memory
{
    public class MemorySendMessages : ISendMessages
    {
        private readonly IReceiveMessages _receiveMessages;

        public MemorySendMessages(IReceiveMessages receiveMessages)
        {
            _receiveMessages = receiveMessages;
        }

        public void Send(IMessage message)
        {
            // todo need to check that we have just one handler, subscriptions?
            _receiveMessages.Receive();
        }
    }
}