using Octo.Core.ServiceBus;

namespace Octo.Core.Cqrs
{
    public interface IEventHandler<TEvent> : IMessageHandler<TEvent> where TEvent : IEvent
    {
    }
}