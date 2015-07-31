using System;

namespace Octo.Core.Cqrs.Common
{
    [Serializable]
    public abstract class Event : IEvent
    {
        public Guid AggregateRootId { get; set; }
    }
}