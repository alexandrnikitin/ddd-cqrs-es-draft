using System;

namespace PushNotifications.Services
{
    public interface IPushNotificationDenormalizationService
    {
        void CreatePushNotification(Guid aggregateRootId, int paymentId);
        void UpdatePushNotificationAsInvalid(Guid aggregateRootId, string description);
        void UpdatePushNotificationAsValid(Guid aggregateRootId);
        void UpdatePushNotificationAsSucceeded(Guid aggregateRootId, string response);
        void UpdatePushNotificationWithUrl(Guid aggregateRootId, string url);
    }
}