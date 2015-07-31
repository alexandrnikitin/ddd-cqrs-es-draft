using System;

using Octo.Core.ServiceBus;

namespace Octo.Core.Cqrs
{
    public interface IEvent : IMessage
    {
        Guid AggregateRootId { get; set; }
    }
}