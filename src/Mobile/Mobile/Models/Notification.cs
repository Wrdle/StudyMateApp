using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public enum NotificationType
    {
        invite, reminder, overdue
    }

    class Notification
    {
        public int Id { get; set; }

        public NotificationType notificationType { get; set; }

        // Can be null in the case of reminders, overdue etc.
        public int SenderID { get; set; }

        public int RecieverID { get; set; }



        public Notification(int id, NotificationType notification_type, int senderID, int recieverID)
        {
            Id = id;
            notificationType = notification_type;
            SenderID = senderID;
            RecieverID = recieverID;
        }

    }
}
