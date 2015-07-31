using Octo.Core.Cqrs.Common;

namespace PushNotifications.Domain.Events
{
    public class PushNotificationSucceededEvent : Event
    {
        public PushNotificationSucceededEvent(string response)
        {
            Response = response;
        }

        public string Response { get; private set; }
    }
}