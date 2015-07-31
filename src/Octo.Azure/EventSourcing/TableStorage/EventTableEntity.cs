using System;
using System.Globalization;

using Microsoft.WindowsAzure.Storage.Table;

using Octo.Core.Cqrs;

namespace Octo.Azure.EventSourcing.TableStorage
{
    public class EventTableEntity : TableEntity
    {
        public EventTableEntity() {}

        public EventTableEntity(ICommittableEvent committableEvent)
            // todo 1341 what about sequence RowKeys with leading zeros like 001, 002?
            : base(committableEvent.Event.AggregateRootId.ToString(), committableEvent.Sequence.ToString(CultureInfo.InvariantCulture))
        {
            DateTime = committableEvent.DateTime;
            EventType = committableEvent.Event.GetType().AssemblyQualifiedName;
            Event = Utility.Jsonize(committableEvent.Event, EventType);
        }

        public DateTime DateTime { get; set; }

        public string Event { get; set; }

        public string EventType { get; set; }
    }
}