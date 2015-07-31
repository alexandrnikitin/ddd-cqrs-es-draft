using Octo.Core.ServiceBus;

namespace Octo.Core.Cqrs.Saga
{
    internal interface IMessageHandlerStartsSaga<TMessage> : IMessageHandler<TMessage>
        where TMessage : IMessage
    {
    }
}