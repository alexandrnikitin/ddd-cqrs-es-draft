using System;
using System.Collections.Generic;

using Octo.Core.EventSourcing;
using Octo.Core.ServiceBus;

namespace Octo.Core.Cqrs.Common
{
    internal class DomainRepository<TAggregateRoot> : IDomainRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot, new()
    {
        private readonly IEventStore<TAggregateRoot> _eventStore;

        // todo 1147 to think about better _internalStore implementation
        private readonly Dictionary<Guid, object> _internalStore = new Dictionary<Guid, object>();

        private readonly Func<IServiceBus> _serviceBusFactory;

        public DomainRepository(Func<IServiceBus> serviceBusFactory, IEventStore<TAggregateRoot> eventStore)
        {
            _serviceBusFactory = serviceBusFactory;
            _eventStore = eventStore;
        }

        public TAggregateRoot GetById(Guid aggregateRootId)
        {
            object value;
            if (_internalStore.TryGetValue(aggregateRootId, out value))
            {
                return (TAggregateRoot)value;
            }

            var aggregateRoot = new TAggregateRoot();

            var events = _eventStore.GetEvents(aggregateRootId);
            aggregateRoot.LoadFromHistoricalEvents(events);

            return aggregateRoot;
        }

        public void Save(IAggregateRoot aggregateRoot)
        {
            var uncommittedEvents = aggregateRoot.UncommittedEvents;

            _eventStore.Insert(uncommittedEvents);

            aggregateRoot.CommitEvents();
            _internalStore[aggregateRoot.Id] = aggregateRoot;

            foreach (var uncommittedEvent in uncommittedEvents)
            {
                _serviceBusFactory().Publish(uncommittedEvent.Event);
            }
        }
    }
}