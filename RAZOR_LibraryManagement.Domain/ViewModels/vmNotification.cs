using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAZOR_LibraryManagement.Lang.Notification;

namespace RAZOR_LibraryManagement.Domain.ViewModels
{
    public class vmNotification
    {
        public string Message { get; set; }
        public NotificationType Type { get; set; }
    }
}
