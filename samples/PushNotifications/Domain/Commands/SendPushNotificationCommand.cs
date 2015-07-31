using System;

using Octo.Core.Cqrs;

namespace PushNotifications.Domain.Commands
{
    public class SendPushNotificationCommand : ICommand
    {
        public SendPushNotificationCommand(Guid pushNotificationId)
        {
            PushNotificationId = pushNotificationId;
        }

        public Guid PushNotificationId { get; private set; }
    }
}