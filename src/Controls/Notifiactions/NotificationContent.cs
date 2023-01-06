using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhai.Famil.Controls.Notifiactions
{
    public class NotificationContent
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public NotificationType Type { get; set; }
    }

    public enum NotificationType   
    {
        Information,
        Success,
        Warning,
        Error
    }
}
