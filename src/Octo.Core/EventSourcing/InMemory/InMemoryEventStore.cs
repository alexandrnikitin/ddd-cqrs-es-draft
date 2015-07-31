using System;
using System.Collections.Generic;
using System.Linq;

using Octo.Core.Cqrs;

namespace Octo.Core.EventSourcing.InMemory
{
    public class InMemoryEventStore<TAggregateRoot> : IEventStore<TAggregateRoot>
    {
        private readonly List<ICommittableEvent> _events = new List<ICommittableEvent>();

        public IEnumerable<ICommittableEvent> GetEvents(Guid aggregateRootId)
        {
            return _events.Where(e => e.Event.AggregateRootId == aggregateRootId);
        }

        public void Insert(IEnumerable<ICommittableEvent> events)
        {
            _events.AddRange(events);
        }
    }
}