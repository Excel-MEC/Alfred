using Radzen;

namespace Alfred.Client.Services.Interfaces
{
    public interface ICustomNotification
    {
        void Notify(NotificationMessage message);
        void Success(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message);
    }
}