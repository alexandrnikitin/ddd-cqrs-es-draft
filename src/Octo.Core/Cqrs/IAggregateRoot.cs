using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Octo.Core.Cqrs
{
    public interface IAggregateRoot
    {
        Guid Id { get; }

        ReadOnlyCollection<ICommittableEvent> UncommittedEvents { get; }

        void Apply(IEvent @event);

        void CommitEvents();

        void LoadFromHistoricalEvents(IEnumerable<ICommittableEvent> committableEvents);
    }
}