namespace Octo.ServiceBus.ServiceBus
{
    public interface IMessageHandler
    {
    }

    public interface IMessageHandler<TMessage> : IMessageHandler
        where TMessage : IMessage
    {
        void Handle(TMessage message);
    }
}