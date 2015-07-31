namespace Octo.ServiceBus.ServiceBus
{
    public interface IServiceBus
    {
        void Publish<TMessage>(TMessage message) where TMessage : IMessage;

        void Receive();

        void Send<TMessage>(TMessage message) where TMessage : IMessage;
    }
}