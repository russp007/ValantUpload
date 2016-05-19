using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ValantExercise.Output;

namespace ValantExercise.Managers
{
    /// <summary>
    /// This class is responsible for sending notifications.
    /// </summary>
    public class NotificationManager : INotificationManager
    {
        // used to write the notification message.
        public INotificationWriter writer;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="writer">The object used to write notifications.</param>
        public NotificationManager(INotificationWriter writer)
        {
            this.writer = writer;
        }

        #region INotificationManager implementation

        /// <summary>
        /// Sends a message to the writer.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public void Send(string message)
        {
            this.writer.WriteLine(message);
        }

        #endregion
    }
}