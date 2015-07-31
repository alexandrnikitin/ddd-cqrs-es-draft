using System;

using Octo.Core.Cqrs;

using PushNotifications.Domain.AggregateRoots;
using PushNotifications.Domain.Commands;

namespace PushNotifications.Domain.CommandHandlers
{
    public class CreatePushNotificationCommandHandler : ICommandHandler<CreatePushNotificationCommand>
    {
        private readonly IDomainRepository<PushNotification> _domainRepository;

        public CreatePushNotificationCommandHandler(IDomainRepository<PushNotification> domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void Handle(CreatePushNotificationCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var pushNotification = new PushNotification(Guid.NewGuid(), command.PaymentId);
            _domainRepository.Save(pushNotification);
        }
    }
}