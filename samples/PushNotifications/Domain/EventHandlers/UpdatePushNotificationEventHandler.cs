using System;

using Octo.Core.Cqrs;

using PushNotifications.Domain.Events;
using PushNotifications.Services;

namespace PushNotifications.Domain.EventHandlers
{
    public class UpdatePushNotificationEventHandler
        : IEventHandler<PushNotificationCreatedEvent>, 
          IEventHandler<PushNotificationInvalidatedEvent>, 
          IEventHandler<PushNotificationValidatedEvent>, 
          IEventHandler<PushNotificationUrlSetEvent>, 
          IEventHandler<PushNotificationSucceededEvent>
    {
        private readonly IPushNotificationDenormalizationService _pushNotificationDenormalizationService;

        public UpdatePushNotificationEventHandler(
            IPushNotificationDenormalizationService pushNotificationDenormalizationService)
        {
            this._pushNotificationDenormalizationService = pushNotificationDenormalizationService;
        }

        public void Handle(PushNotificationCreatedEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");

            _pushNotificationDenormalizationService.CreatePushNotification(@event.AggregateRootId, @event.PaymentId);
        }

        public void Handle(PushNotificationInvalidatedEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");

            _pushNotificationDenormalizationService.UpdatePushNotificationAsInvalid(
                @event.AggregateRootId, 
                @event.Error);
        }

        public void Handle(PushNotificationValidatedEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");

            _pushNotificationDenormalizationService.UpdatePushNotificationAsValid(@event.AggregateRootId);
        }

        public void Handle(PushNotificationUrlSetEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");

            _pushNotificationDenormalizationService.UpdatePushNotificationWithUrl(@event.AggregateRootId, @event.Url);
        }

        public void Handle(PushNotificationSucceededEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");

            _pushNotificationDenormalizationService.UpdatePushNotificationAsSucceeded(
                @event.AggregateRootId, 
                @event.Response);
        }
    }
}