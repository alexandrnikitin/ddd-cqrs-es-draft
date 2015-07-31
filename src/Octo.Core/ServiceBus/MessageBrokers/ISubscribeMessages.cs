using System;

namespace Octo.Core.ServiceBus.MessageBrokers
{
    public interface ISubscribeMessages
    {
        void Subscribe(Type messageType, Type messageHandler);
    }
}