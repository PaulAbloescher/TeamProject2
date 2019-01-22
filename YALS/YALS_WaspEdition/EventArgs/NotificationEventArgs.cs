using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YALS_WaspEdition.MyEventArgs
{
    public class NotificationEventArgs
    {
        public NotificationEventArgs(string message, string caption, MessageBoxButton msgButton, MessageBoxImage icon)
        {
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
            this.Caption = caption ?? throw new ArgumentNullException(nameof(caption));
            this.MessageBoxBtn = msgButton;
            this.Icon = icon;
        }

        public string Message
        {
            get;
        }       

        public string Caption
        {
            get;
        }

        public MessageBoxButton MessageBoxBtn
        {
            get;
        }

        public MessageBoxImage Icon
        {
            get;
        }
    }
}
