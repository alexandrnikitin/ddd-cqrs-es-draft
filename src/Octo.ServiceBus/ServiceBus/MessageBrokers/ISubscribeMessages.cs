using System;

namespace Octo.ServiceBus.ServiceBus.MessageBrokers
{
    public interface ISubscribeMessages
    {
        void Subscribe(Type messageType, Type messageHandler);
    }
}