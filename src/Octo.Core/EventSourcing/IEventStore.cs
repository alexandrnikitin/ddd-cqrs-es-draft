using System;
using System.Collections.Generic;

using Octo.Core.Cqrs;

namespace Octo.Core.EventSourcing
{
    public interface IEventStore<TAggregateRoot>
    {
        IEnumerable<ICommittableEvent> GetEvents(Guid aggregateRootId);

        void Insert(IEnumerable<ICommittableEvent> events);
    }
}