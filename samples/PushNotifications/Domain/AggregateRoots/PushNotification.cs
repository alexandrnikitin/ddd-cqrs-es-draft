using System;

using Octo.Core.Cqrs.Common;

using PushNotifications.Domain.Events;

namespace PushNotifications.Domain.AggregateRoots
{
    public class PushNotification : AggregateRoot
    {
        public PushNotification(Guid id, int paymentId)
        {
            Apply(new PushNotificationCreatedEvent(paymentId) { AggregateRootId = id });
        }

        public PushNotification()
        {
        }

        public string Description { get; private set; }

        public int PaymentId { get; private set; }

        public bool Succeeded { get; private set; }

        public string Url { get; private set; }

        public bool Validated { get; private set; }

        // ReSharper disable once UnusedMember.Global
        public void Invalidate(string error)
        {
            Apply(new PushNotificationInvalidatedEvent(error));
        }

        // ReSharper disable once UnusedMember.Global
        public void OnPushNotificationCreated(PushNotificationCreatedEvent @event)
        {
            Id = @event.AggregateRootId;
            PaymentId = @event.PaymentId;
        }

        // ReSharper disable once UnusedMember.Global
        public void OnPushNotificationInvalidated(PushNotificationInvalidatedEvent @event)
        {
            Validated = false;
            Description = @event.Error;
        }

        // ReSharper disable once UnusedMember.Global
        public void OnPushNotificationSucceeded(PushNotificationSucceededEvent @event)
        {
            Succeeded = true;
            Description = @event.Response;
        }

        // ReSharper disable once UnusedMember.Global
        public void OnPushNotificationUrlSet(PushNotificationUrlSetEvent @event)
        {
            Url = @event.Url;
        }

        // ReSharper disable once UnusedMember.Global
        public void OnPushNotificationValidated(PushNotificationValidatedEvent @event)
        {
            Validated = true;
        }

        public void SetUrlTo(string url)
        {
            Apply(new PushNotificationUrlSetEvent(url));
        }

        public void Succeed(string response)
        {
            Apply(new PushNotificationSucceededEvent(response));
        }

        public void Validate()
        {
            Apply(new PushNotificationValidatedEvent());
        }
    }
}