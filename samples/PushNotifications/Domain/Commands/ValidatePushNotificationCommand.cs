using System;

using Octo.Core.Cqrs;

namespace PushNotifications.Domain.Commands
{
    public class ValidatePushNotificationCommand : ICommand
    {
        public ValidatePushNotificationCommand(Guid pushNotificationId)
        {
            PushNotificationId = pushNotificationId;
        }

        public Guid PushNotificationId { get; private set; }
    }
}