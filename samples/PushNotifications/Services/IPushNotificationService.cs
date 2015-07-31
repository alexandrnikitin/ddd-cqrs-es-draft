namespace PushNotifications.Services
{
    public interface IPushNotificationService
    {
        string GetUrlFor(int paymentId, string url);

        bool TrySend(string url, out string response);

        bool IsValid(int paymentId, out string error, out string url);
    }
}