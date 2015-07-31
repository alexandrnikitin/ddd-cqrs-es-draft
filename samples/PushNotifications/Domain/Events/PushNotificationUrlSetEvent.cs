using Octo.Core.Cqrs.Common;

namespace PushNotifications.Domain.Events
{
    public class PushNotificationUrlSetEvent : Event
    {
        public PushNotificationUrlSetEvent(string url)
        {
            Url = url;
        }

        public string Url { get; private set; }
    }
}