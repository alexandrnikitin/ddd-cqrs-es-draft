namespace Octo.Core.ServiceBus.MessageBrokers
{
    public interface ISendMessages
    {
        void Send(IMessage message);
    }
}