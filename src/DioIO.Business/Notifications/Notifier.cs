using DioIO.Business.Interface;
using System.Collections.Generic;
using System.Linq;

namespace DioIO.Business.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notification;

        public Notifier()
        {
            _notification = new List<Notification>();
        }

        public void Handle(Notification notification)
        {
            _notification.Add(notification);
        }

        public List<Notification> GetNotifications()
        {
            return _notification;
        }

        public bool IsThereNotification()
        {
            return _notification.Any();
        }
    }
}
