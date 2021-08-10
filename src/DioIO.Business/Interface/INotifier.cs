using DioIO.Business.Notifications;
using System.Collections.Generic;

namespace DioIO.Business.Interface
{
    public interface INotifier
    {
        bool IsThereNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
        //Handle maniputar

    }
}
