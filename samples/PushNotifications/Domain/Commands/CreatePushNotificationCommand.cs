using Octo.Core.Cqrs;

namespace PushNotifications.Domain.Commands
{
    public class CreatePushNotificationCommand : ICommand
    {
        public CreatePushNotificationCommand(int paymentId)
        {
            PaymentId = paymentId;
        }

        public int PaymentId { get; private set; }
    }
}