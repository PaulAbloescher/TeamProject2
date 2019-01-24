// <copyright file="NotificationEventArgs.cs" company="KW Softworks">
//     Copyright (c) Paul-Noel Ablöscher. All rights reserved.
// </copyright>
// <summary>Represents the NotificationEventArgs class.</summary>
// <author>Killerwasps</author>

namespace YALS_WaspEdition.MyEventArgs
{
    using System;
    using System.Windows;

    /// <summary>
    /// Represents the NotificationEventArgs class.
    /// </summary>
    public class NotificationEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationEventArgs"/> class.
        /// </summary>
        /// <param name="message">Represents message.</param>
        /// <param name="caption">Represents caption.</param>
        /// <param name="msgButton">Represents MSG button.</param>
        /// <param name="icon">Represents icon.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the message or caption is null.
        /// </exception>
        public NotificationEventArgs(string message, string caption, MessageBoxButton msgButton, MessageBoxImage icon)
        {
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
            this.Caption = caption ?? throw new ArgumentNullException(nameof(caption));
            this.MessageBoxBtn = msgButton;
            this.Icon = icon;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get;
        }

        /// <summary>
        /// Gets the caption.
        /// </summary>
        /// <value>
        /// The caption.
        /// </value>
        public string Caption
        {
            get;
        }

        /// <summary>
        /// Gets the message box BTN.
        /// </summary>
        /// <value>
        /// The message box BTN.
        /// </value>
        public MessageBoxButton MessageBoxBtn
        {
            get;
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>
        /// Represents the icon.
        /// </value>
        public MessageBoxImage Icon
        {
            get;
        }
    }
}
