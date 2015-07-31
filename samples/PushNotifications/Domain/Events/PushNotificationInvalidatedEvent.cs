using Octo.Core.Cqrs.Common;

namespace PushNotifications.Domain.Events
{
    public class PushNotificationInvalidatedEvent : Event
    {
        public PushNotificationInvalidatedEvent(string error)
        {
            Error = error;
        }

        public string Error { get; private set; }
    }
}