using Octo.Core.Cqrs.Common;

namespace PushNotifications.Domain.Events
{
    public class PushNotificationCreatedEvent : Event
    {
        public PushNotificationCreatedEvent(int paymentId)
        {
            PaymentId = paymentId;
        }

        public int PaymentId { get; private set; }
    }
}