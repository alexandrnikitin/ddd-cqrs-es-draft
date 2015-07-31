using System;

using Octo.Core.Cqrs;

using PushNotifications.Domain.AggregateRoots;
using PushNotifications.Domain.Commands;
using PushNotifications.Services;

namespace PushNotifications.Domain.CommandHandlers
{
    public class SendPushNotificationCommandHandler : ICommandHandler<SendPushNotificationCommand>
    {
        private readonly IDomainRepository<PushNotification> _domainRepository;

        private readonly IPushNotificationService _pushNotificationService;

        public SendPushNotificationCommandHandler(
            IDomainRepository<PushNotification> domainRepository, 
            IPushNotificationService pushNotificationService)
        {
            _domainRepository = domainRepository;
            _pushNotificationService = pushNotificationService;
        }

        public void Handle(SendPushNotificationCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var pushNotification = _domainRepository.GetById(command.PushNotificationId);
            var paymentId = pushNotification.PaymentId;
            var url = _pushNotificationService.GetUrlFor(paymentId, pushNotification.Url);

            pushNotification.SetUrlTo(url);
            _domainRepository.Save(pushNotification);

            string response;
            if (_pushNotificationService.TrySend(url, out response))
            {
                pushNotification.Succeed(response);
                _domainRepository.Save(pushNotification);
            }
            else
            {
                // description only?
                // NotSucceed?
            }

            // todo
        }
    }
}