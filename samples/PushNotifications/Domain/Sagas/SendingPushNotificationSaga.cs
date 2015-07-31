using System;

using Octo.Core.Cqrs;
using Octo.Core.ServiceBus;

using PushNotifications.Domain.Commands;
using PushNotifications.Domain.Events;

namespace PushNotifications.Domain.Sagas
{
    public class SendingPushNotificationSaga
        :
            IEventHandler<PaymentReceivedEvent>, 
            IEventHandler<PushNotificationCreatedEvent>, 
            IEventHandler<PushNotificationValidatedEvent>
    {
        private readonly IServiceBus _serviceBus;

        public SendingPushNotificationSaga(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        public void Handle(PaymentReceivedEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");

            _serviceBus.Publish(new CreatePushNotificationCommand(@event.PaymentId));
        }

        public void Handle(PushNotificationCreatedEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");

            _serviceBus.Publish(new ValidatePushNotificationCommand(@event.AggregateRootId));
        }

        public void Handle(PushNotificationValidatedEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");

            _serviceBus.Publish(new SendPushNotificationCommand(@event.AggregateRootId));
        }
    }
}