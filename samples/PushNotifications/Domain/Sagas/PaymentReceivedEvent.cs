using System;

using Octo.Core.Cqrs;

namespace PushNotifications.Domain.Sagas
{
    public class PaymentReceivedEvent : IEvent
    {
        public Guid AggregateRootId { get; set; }
        public int PaymentId { get; set; }
    }
}