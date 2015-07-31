namespace Octo.ServiceBus.ServiceBus.MessageBrokers
{
    public interface ISendMessages
    {
        void Send(IMessage message);
    }
}