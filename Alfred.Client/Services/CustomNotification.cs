using Alfred.Client.Services.Interfaces;
using Radzen;

namespace Alfred.Client.Services
{
    public class CustomNotification: ICustomNotification
    {
        private readonly NotificationService _notification;

        public CustomNotification(NotificationService notification)
        {
            _notification = notification;
        }

        public void Notify(NotificationMessage message)
        {
            _notification.Notify(message);
        }

        public void Success(string message)
        {
            _notification.Notify(Message(NotificationSeverity.Success, "Success", message));
        }

        public void Info(string message)
        {
            _notification.Notify(Message(NotificationSeverity.Info, "Info", message));
        }

        public void Warning(string message)
        {
            _notification.Notify(Message(NotificationSeverity.Warning, "Warning", message));
        }

        public void Error(string message)
        {
            _notification.Notify(Message(NotificationSeverity.Error, "Warning", message));
        }

        private NotificationMessage Message(NotificationSeverity severity, string summary, string message)
        {
            return new NotificationMessage()
            {
                Severity = severity,
                Summary = summary,
                Detail = message,
                Duration = 4000
            };
        }
    }
    
}