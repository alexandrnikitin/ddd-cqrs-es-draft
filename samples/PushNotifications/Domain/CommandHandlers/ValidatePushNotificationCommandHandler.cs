using System;

using Octo.Core.Cqrs;

using PushNotifications.Domain.AggregateRoots;
using PushNotifications.Domain.Commands;
using PushNotifications.Services;

namespace PushNotifications.Domain.CommandHandlers
{
    public class ValidatePushNotificationCommandHandler : ICommandHandler<ValidatePushNotificationCommand>
    {
        private readonly IDomainRepository<PushNotification> _domainRepository;
        private readonly IPushNotificationService _pushNotificationService;

        public ValidatePushNotificationCommandHandler(
            IPushNotificationService pushNotificationService, 
            IDomainRepository<PushNotification> domainRepository)
        {
            _pushNotificationService = pushNotificationService;
            _domainRepository = domainRepository;
        }

        public void Handle(ValidatePushNotificationCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var pushNotification = _domainRepository.GetById(command.PushNotificationId);

            string error;
            string url;

            if (_pushNotificationService.IsValid(pushNotification.PaymentId, out error, out url))
            {
                pushNotification.SetUrlTo(url);
                pushNotification.Validate();
            }
            else
            {
                pushNotification.Invalidate(error);
            }

            _domainRepository.Save(pushNotification);
        }
    }
}