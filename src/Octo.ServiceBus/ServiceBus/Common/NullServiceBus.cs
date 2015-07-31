namespace Octo.ServiceBus.ServiceBus.Common
{
    internal class NullServiceBus : IServiceBus
    {
        public void Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            throw new System.NotImplementedException();
        }

        public void Receive()
        {
            throw new System.NotImplementedException();
        }

        public void Send<TMessage>(TMessage message) where TMessage : IMessage
        {
            throw new System.NotImplementedException();
        }
    }
}