using System;

namespace Octo.Core.Cqrs.Common
{
    public class CommittableEvent : ICommittableEvent
    {
        public CommittableEvent(IEvent @event, long sequence, DateTime dateTime)
        {
            Event = @event;
            Sequence = sequence;
            DateTime = dateTime;
        }

        public DateTime DateTime { get; private set; }

        public IEvent Event { get; private set; }

        public long Sequence { get; private set; }
    }
}