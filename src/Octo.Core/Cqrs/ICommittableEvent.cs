using System;

namespace Octo.Core.Cqrs
{
    public interface ICommittableEvent
    {
        DateTime DateTime { get; }

        IEvent Event { get; }

        long Sequence { get; }
    }
}